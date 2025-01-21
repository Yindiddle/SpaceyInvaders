using UnityEngine;

public class WorldBoundary : MonoBehaviour
{
    // These static fields store the camera's bounding edges
    public static float LeftEdge;
    public static float RightEdge;
    public static float TopEdge;
    public static float BottomEdge;

    void Awake()
    {
        // We assume there's only one main camera
        Camera cam = Camera.main;

        // Convert the camera's viewport corners to world space
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0f, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1f, 1f, cam.nearClipPlane));

        LeftEdge = bottomLeft.x;
        BottomEdge = bottomLeft.y;
        RightEdge = topRight.x;
        TopEdge = topRight.y;
    }
}
