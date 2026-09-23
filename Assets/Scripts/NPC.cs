using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public NPCDialogue returnedDiaryDialogue;
    public NPCDialogue secondQuestDialogue;
    public NPCDialogue thirdQuestDialogue;

    [Header("Only for NPCs that should stay hidden until a condition is met")]
    public bool requiresDiaryReadAgain = false;

    public event Action OnDialogueEnded;

    private DialogueController dialogueUI;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private void Start()
    {
        dialogueUI = DialogueController.Instance;

        if (requiresDiaryReadAgain)
        {
            Debug.Log($"[{gameObject.name}] Start() - GameState.DiaryReadAgain = {GameState.DiaryReadAgain}");
            gameObject.SetActive(GameState.DiaryReadAgain);

            if (!GameState.DiaryReadAgain)
            {
                // Stay hidden until the condition is met; don't evaluate dialogue priority yet.
                return;
            }
        }

        if (GameState.ThirdQuestCompleted && thirdQuestDialogue != null)
        {
            dialogueData = thirdQuestDialogue;
        }
        else if (GameState.SecondQuestCompleted && secondQuestDialogue != null)
        {
            dialogueData = secondQuestDialogue;
        }
        else if (GameState.DiaryReturned && returnedDiaryDialogue != null)
        {
            dialogueData = returnedDiaryDialogue;
        }
        else if (!requiresDiaryReadAgain)
        {
            gameObject.SetActive(true);
        }
    }

    public bool CanInteract()
    {
        return true;
    }
    
    public void Interact()
    {
        if (dialogueData == null)
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait);
        dialogueUI.ShowDialogueUI(true);
        DisplayCurrentLine();
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;

            // If this line has choices, show them immediately after finishing typing.
            dialogueUI.ClearChoices();
            if (TryDisplayChoicesForCurrentIndex())
                return;

            // Do not auto-advance on the same click that skipped typing.
            return;
        }

        dialogueUI.ClearChoices();

        // Prioritize showing choices for current line.
        if (TryDisplayChoicesForCurrentIndex())
            return;

        // End-check after choices.
        if (dialogueData.endDialogueLines.Length > dialogueIndex &&
            dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    bool TryDisplayChoicesForCurrentIndex()
    {
        if (dialogueData.choices == null) return false;

        foreach (DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if (dialogueChoice.DialogueIndex != dialogueIndex) continue;

            if (dialogueChoice.choices == null || dialogueChoice.nextDialogueIndexs == null)
            {
                Debug.LogWarning($"Choices missing arrays at dialogue index {dialogueIndex}");
                return false;
            }

            if (dialogueChoice.choices.Length != dialogueChoice.nextDialogueIndexs.Length)
            {
                Debug.LogWarning($"Choice/next-index length mismatch at dialogue index {dialogueIndex}");
                return false;
            }

            DisplayChoices(dialogueChoice);
            Debug.Log($"Displayed choices at dialogue index {dialogueIndex}");
            return true;
        }

        return false;
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);

            if (!char.IsWhiteSpace(letter))
            {
                AudioManager.Instance.PlayRandomDialogueBlip();
            }

            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        // Show choices as soon as line finishes typing.
        dialogueUI.ClearChoices();
        if (TryDisplayChoicesForCurrentIndex())
            yield break;

        if (dialogueData.autoProgressLines.Length > dialogueIndex &&
            dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexs[i];
            dialogueUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
        }
    }

    void ChooseOption(int nextIndex)
    {
        if (nextIndex < 0 || nextIndex >= dialogueData.dialogueLines.Length)
        {
            Debug.LogWarning($"Invalid next dialogue index: {nextIndex}");
            return;
        }

        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        Debug.Log("Closing dialogue for: " + dialogueData.npcName);

        OnDialogueEnded?.Invoke();
    }
}