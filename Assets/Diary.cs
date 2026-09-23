using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Diary : MonoBehaviour, IInteractable
{
    public InteractionMessage interactionMessage;
    public Tilemap diaryTilemap;

    [TextArea]
    public string firstReadPage1 = "I don't know how much longer I can stay here.";
    [TextArea]
    public string firstReadPage2 = "Miss says everything is going to be okay, but I don't believe her.";

    [TextArea]
    public string secondReadPage1 = "Brian asked me if I remember him";
    [TextArea]
    public string secondReadPage2 = "I said yes. I should've said no";

    [Header("Pagination Timing")]
    [Tooltip("How long page 1 stays visible before automatically switching to page 2.")]
    public float pageDelay = 3f;

    private bool isReading = false;

    private void Start()
    {
        if (GameState.ThirdQuestCompleted)
        {
            gameObject.SetActive(true);
        }
        else if (GameState.DiaryReturned)
        {
            gameObject.SetActive(false);
        }
    }

    public bool CanInteract()
    {
        return !isReading;
    }

    public void Interact()
    {
        if (isReading) return;

        StartCoroutine(ReadDiaryRoutine());
    }

    private IEnumerator ReadDiaryRoutine()
    {
        isReading = true;

        bool isSecondRead = GameState.ThirdQuestCompleted;

        string page1 = isSecondRead ? secondReadPage1 : firstReadPage1;
        string page2 = isSecondRead ? secondReadPage2 : firstReadPage2;

        interactionMessage.ShowMessage(page1);

        yield return new WaitForSeconds(pageDelay);

        interactionMessage.ShowMessage(page2);

        if (isSecondRead)
        {
            GameState.DiaryReadAgain = true;
        }
        else
        {
            diaryTilemap.SetTile(new Vector3Int(-30, 26, 0), null);
            diaryTilemap.SetTile(new Vector3Int(-29, 26, 0), null);

            GameState.DiaryReturned = true;
        }

        AudioManager.Instance.RefreshMusicForQuestProgress();

        // Wait for the second page's own display duration before hiding the diary,
        // so it doesn't disappear while page 2 is still showing.
        yield return new WaitForSeconds(interactionMessage.displayDuration);

        gameObject.SetActive(false);
        isReading = false;
    }
}