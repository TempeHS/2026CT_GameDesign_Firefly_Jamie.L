using System.Collections;
using TMPro;
using UnityEngine;

public class InteractionMessage : MonoBehaviour
{
    [Header("References")]
    public GameObject messageObject;
    public TMP_Text messageText;
    public CanvasGroup canvasGroup;

    [Header("Settings")]
    [Tooltip("How long the message stays fully visible before fading out.")]
    public float displayDuration = 4f;

    [Tooltip("How long the fade in/out transition takes.")]
    public float fadeDuration = 0.3f;

    private Coroutine activeRoutine;

    private void Awake()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;

        if (activeRoutine != null)
        {
            StopCoroutine(activeRoutine);
        }

        activeRoutine = StartCoroutine(ShowMessageRoutine());
    }

    private IEnumerator ShowMessageRoutine()
    {
        if (canvasGroup != null) canvasGroup.blocksRaycasts = true;

        yield return Fade(0f, 1f);

        yield return new WaitForSeconds(displayDuration);

        yield return Fade(1f, 0f);

        if (canvasGroup != null) canvasGroup.blocksRaycasts = false;
    }

    private IEnumerator Fade(float from, float to)
    {
        if (canvasGroup == null)
        {
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}