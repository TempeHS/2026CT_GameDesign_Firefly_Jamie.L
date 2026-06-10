using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Vector3 cameraTargetPosition;
    public float transitionTime = 0.5f;

    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(MoveCamera());
        }
    }

    private System.Collections.IEnumerator MoveCamera()
    {
        isTransitioning = true;

        Camera cam = Camera.main;
        Vector3 startPos = cam.transform.position;
        Vector3 endPos = cameraTargetPosition;

        float elapsed = 0f;

        while (elapsed < transitionTime)
        {
            cam.transform.position = Vector3.Lerp(startPos, endPos, elapsed / transitionTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = endPos;
        isTransitioning = false;
    }
}
