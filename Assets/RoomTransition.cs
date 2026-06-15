using System.Collections;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Vector3 cameraTargetPosition;
    public float transitionTime = 0.5f;

    // NEW: boundaries for the next room
    public Transform newBottomLeft;
    public Transform newTopRight;

    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(MoveCamera());
        }
    }

    private IEnumerator MoveCamera()
    {
        isTransitioning = true;

        Camera cam = Camera.main;
        CameraFollow follow = cam.GetComponent<CameraFollow>();

        // Freeze camera follow
        follow.isFrozen = true;

        // Update boundaries BEFORE unfreezing
        follow.bottomLeft = newBottomLeft;
        follow.topRight = newTopRight;
        follow.UpdateBounds();

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

        // Unfreeze camera follow
        follow.isFrozen = false;
        isTransitioning = false;
    }
}
