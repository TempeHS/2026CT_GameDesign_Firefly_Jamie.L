using UnityEngine;

public class MemoryPaper : MonoBehaviour, IInteractable
{
    [TextArea]
    public string paperText;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        PaperUI.Instance.Open(paperText);
    }
}
