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
        Debug.Log("MemoryPaper Interact called");
        Debug.Log("PaperUI.Instance = " + PaperUI.Instance);
        PaperUI.Instance.Open(paperText);
    }

}
