using UnityEngine;
using UnityEngine.Tilemaps;

public class Diary : MonoBehaviour, IInteractable
{
    public InteractionMessage interactionMessage;
    public Tilemap diaryTilemap;

    private void Start()
    {
        if (GameState.ThirdQuestCompleted)
        {
            // NPC3's quest is done - diary reappears for its second read.
            gameObject.SetActive(true);
        }
        else if (GameState.DiaryReturned)
        {
            // Diary was already read once, but NPC3's quest isn't done yet - stay hidden.
            gameObject.SetActive(false);
        }
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (GameState.ThirdQuestCompleted)
        {
            interactionMessage.ShowMessage(
                "Brian asked me if I would remember him" +
                "I said yes. I don't think I should've"
            );

            GameState.DiaryReadAgain = true;

            gameObject.SetActive(false);
        }
        else
        {
            interactionMessage.ShowMessage(
                "I don't know how much longer I can stay here. " +
                "Miss says everything is going to be okay, but I don't believe her."
            );

            diaryTilemap.SetTile(new Vector3Int(-30, 26, 0), null);
            diaryTilemap.SetTile(new Vector3Int(-29, 26, 0), null);

            GameState.DiaryReturned = true;

            gameObject.SetActive(false);
        }
    }
}