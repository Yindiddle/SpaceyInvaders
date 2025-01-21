using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private float lifetime;
    private bool hit;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = Time.deltaTime * speed;
        transform.Translate(0 , movementSpeed, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;

        if (collision.tag == "Invader")
        {

            collision.gameObject.GetComponent<Health>().TakeDamage(1);
            Deactivate();
        } 
    }
    
    public void Shoot()
    {
        if (!PauseManager.Instance.IsPaused)
        {
            lifetime = 0;
            gameObject.SetActive(true);
            hit = false;
            boxCollider.enabled = true;
        }        
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
