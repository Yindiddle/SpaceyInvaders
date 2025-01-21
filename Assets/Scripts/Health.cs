using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] float destroyDelay = 1f;

    public int health;
    private Animator anim;
    private AudioSource audioSource;
    private BoxCollider2D boxCol;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        boxCol = GetComponent<BoxCollider2D>();
        audioSource.playOnAwake = false;
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            anim.SetTrigger("explodes");
            audioSource.Play();
            boxCol.enabled = false;
            StartCoroutine(DestroyAfterDelay());
            ScoreManager.Instance.IncreaseScore(1);
        }
    }

    public IEnumerator DestroyAfterDelay()
    {
        // Wait for 'destroyDelay' seconds
        yield return new WaitForSeconds(destroyDelay);

        // Destroy the target if it's still valid
        Destroy(gameObject);
    }
}
