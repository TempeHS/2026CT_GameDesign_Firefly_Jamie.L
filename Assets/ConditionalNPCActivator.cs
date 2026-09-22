using UnityEngine;

public class ConditionalNPCActivator : MonoBehaviour
{
    public GameObject npcToActivate;

    private void Update()
    {
        if (GameState.DiaryReadAgain && !npcToActivate.activeSelf)
        {
            npcToActivate.SetActive(true);
        }
    }
}