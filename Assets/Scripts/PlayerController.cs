using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    //[SerializeField] float boundaryPadding = 0.5f;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private float cooldownDuration;
    [SerializeField] private Transform firePoint;

       
    private AudioSource audioSource;

    // Private variables
    private Rigidbody2D rb;
    private float cooldownTimer;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PlayerMove();

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            PlayerShoot();
        cooldownTimer += Time.deltaTime;
    }
    private void PlayerMove()
    {
        float moveDirection = 0f;

        if (Input.GetKey(KeyCode.A) && WorldBoundary.LeftEdge + 1 < transform.position.x)
        {
            moveDirection = -1f; // Move Left
        }
        else if (Input.GetKey(KeyCode.D) && WorldBoundary.RightEdge - 1 > transform.position.x)
        {
            moveDirection = 1f; // Move Right
        }

        Vector2 velocity = rb.velocity;
        velocity.x = moveDirection * moveSpeed;
        rb.velocity = velocity;
    }

    private void PlayerShoot()
    {
        if (cooldownTimer >= cooldownDuration && !PauseManager.Instance.IsPaused)
        {
            //SoundManager.instance.PlaySound(fireBallSound);
            //anim.SetTrigger("attack");
            cooldownTimer = 0;
            bullets[FindBullet()].transform.position = firePoint.position;
            bullets[FindBullet()].GetComponent<Bullet>().Shoot();
            audioSource.Play();
        }


        
      
        

    }

    private int FindBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
