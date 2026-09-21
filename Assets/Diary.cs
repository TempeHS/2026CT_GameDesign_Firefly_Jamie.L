using UnityEngine;
using UnityEngine.Tilemaps;

public class Diary : MonoBehaviour, IInteractable
{
    public InteractionMessage interactionMessage;
    public Tilemap diaryTilemap;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
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