using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;

    [Header("Fallbacks")]
    [SerializeField] private Sprite defaultPortrait;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (choiceContainer == null || dialoguePanel == null) return;

        // Force container to be inside dialogue panel hierarchy
        if (!choiceContainer.IsChildOf(dialoguePanel.transform))
        {
            choiceContainer.SetParent(dialoguePanel.transform, false);
            Debug.LogWarning("choiceContainer was outside dialoguePanel. Reparented automatically.");
        }

        if (choiceContainer.GetComponent<VerticalLayoutGroup>() == null)
        {
            Debug.LogWarning("Choice Container is missing VerticalLayoutGroup.");
        }

        if (portraitImage == null)
        {
            Debug.LogWarning("DialogueController: portraitImage is not assigned.");
        }
    }

    public void ShowDialogueUI(bool show) => dialoguePanel.SetActive(show);

    public void SetNPCInfo(string npc, Sprite portrait)
    {
        if (nameText != null) nameText.text = npc;

        if (portraitImage == null) return;

        // Prevent clearing the portrait when portrait is null.
        if (portrait != null)
        {
            portraitImage.sprite = portrait;
            portraitImage.enabled = true;
        }
        else if (defaultPortrait != null)
        {
            portraitImage.sprite = defaultPortrait;
            portraitImage.enabled = true;
            Debug.LogWarning($"SetNPCInfo received null portrait for '{npc}'. Using default portrait.");
        }
        else
        {
            Debug.LogWarning($"SetNPCInfo received null portrait for '{npc}', and no defaultPortrait is set.");
        }
    }

    public void SetDialogueText(string text) => dialogueText.text = text;

    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer) Destroy(child.gameObject);
    }

    public GameObject CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer, false);

        var btn = choiceButton.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(onClick);

        var label = choiceButton.GetComponentInChildren<TMP_Text>();
        if (label != null) label.text = choiceText;

        LayoutRebuilder.ForceRebuildLayoutImmediate(choiceContainer as RectTransform);
        return choiceButton;
    }
}