using UnityEngine;

public class FlyInScript : MonoBehaviour
{
    public Vector3 TargetPosition;
    public float Speed = 2f;

    private void Update()
    {
        // Smoothly move toward the target position
        transform.position = Vector3.Lerp(transform.position, TargetPosition, Speed * Time.deltaTime);

        // Optional: Destroy this script once the target is reached
        if (Vector3.Distance(transform.position, TargetPosition) < 0.01f)
        {
            Destroy(this);
        }
    }
}
