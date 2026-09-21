using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        Debug.Log("You can't interact with this.");
    }
}