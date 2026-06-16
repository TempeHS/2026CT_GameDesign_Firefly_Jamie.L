using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform bottomLeft;
    public Transform topRight;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    float minX, maxX, minY, maxY;

    void Start()
    {
        minX = bottomLeft.position.x;
        minY = bottomLeft.position.y;
        maxX = topRight.position.x;
        maxY = topRight.position.y;
    }

    void LateUpdate()
    {
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
