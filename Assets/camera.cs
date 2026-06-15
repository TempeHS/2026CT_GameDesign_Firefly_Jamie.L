using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // These change per room
    public Transform bottomLeft;
    public Transform topRight;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    float minX, maxX, minY, maxY;

    public bool isFrozen = false;

    void Start()
    {
        UpdateBounds();
    }

    public void UpdateBounds()
    {
        minX = bottomLeft.position.x;
        minY = bottomLeft.position.y;
        maxX = topRight.position.x;
        maxY = topRight.position.y;
    }

    void LateUpdate()
    {
        if (isFrozen) return;

        Vector3 desired = player.position + offset;

        float clampedX = Mathf.Clamp(desired.x, minX, maxX);
        float clampedY = Mathf.Clamp(desired.y, minY, maxY);

        Vector3 smoothed = Vector3.Lerp(
            transform.position,
            new Vector3(clampedX, clampedY, transform.position.z),
            smoothSpeed
        );

        transform.position = smoothed;
    }
}
