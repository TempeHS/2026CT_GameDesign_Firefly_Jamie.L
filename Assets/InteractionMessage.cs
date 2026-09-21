using UnityEngine;
using TMPro;
using System.Collections;

public class InteractionMessage : MonoBehaviour
{
    public GameObject messageObject;
    public TMP_Text messageText;

    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageObject.SetActive(true);

        StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(4f);
        messageObject.SetActive(false);
    }
}