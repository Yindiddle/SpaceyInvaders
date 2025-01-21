using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Invader : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 nextPos;

    void Start() => rb = GetComponent<Rigidbody2D>();

    // Listen for the pulse and choose a new "nextPos"
    void OnEnable() => GameManager.OnPulse += SetNextPos;
    void OnDisable() => GameManager.OnPulse -= SetNextPos;

    void SetNextPos()
    {
        // Move 1 unit to the right
        nextPos = transform.position + Vector3.right;
    }

    void FixedUpdate()
    {
        // If we haven't reached nextPos, keep adding force
        Vector2 diff = nextPos - transform.position;
        if (diff.sqrMagnitude > 0.01f) // 0.01 ~ "close enough"
            rb.AddForce(diff.normalized * 10f);  // Tweak 10f for speed/inertia
    }
}
