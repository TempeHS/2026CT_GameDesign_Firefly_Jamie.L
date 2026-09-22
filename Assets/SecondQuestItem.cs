using UnityEngine;

public class SecondQuestItem : MonoBehaviour, IInteractable
{
    public InteractionMessage interactionMessage;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (interactionMessage != null)
        {
            if (GameState.SecondQuestCompleted)
            {
                interactionMessage.ShowMessage("A photo with everybody from the orphanage. Connor, Khoi, Matthew and Wolfgang");
            }
            else
            {
                interactionMessage.ShowMessage("A photo with everybody from the orphanage. Connor, Khoi, Matthew, Wolfgang and Brian");
            }
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: interactionMessage is not assigned in the Inspector.", this);
        }

        if (GameState.SecondQuestCompleted)
        {
            // Player has already collected this photo once before (for NPC2).
            // Collecting it again unlocks NPC3's quest.
            GameState.ThirdQuestCompleted = true;
        }
        else
        {
            GameState.SecondQuestCompleted = true;
        }

        gameObject.SetActive(false);
    }
}
