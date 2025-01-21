using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Vector3 targetPos;
    private bool isMoving;

    private bool justMovedDown;


    void OnEnable() => GameManager.OnPulse += OnPulse;
    void OnDisable() => GameManager.OnPulse -= OnPulse;
    
    void Start()
    {
        speed = 6;
    }

    void OnPulse()
    {
        //Are we on even/odd row
        bool isEvenRow = (Mathf.FloorToInt(transform.position.y) % 2 == 0);
        //Are we at right/left walls
        bool nearRightEdge = WorldBoundary.RightEdge - transform.position.x < 1.1;
        bool nearLeftEdge = transform.position.x - WorldBoundary.LeftEdge < 1.1;

        //Evens move down on the right
        if (isEvenRow && nearRightEdge && !justMovedDown)
        {
            targetPos = transform.position + Vector3.down;
            justMovedDown = true;
        }

        //Odds move down on the left
        else if (!isEvenRow && nearLeftEdge && !justMovedDown)
        {
            targetPos = transform.position + Vector3.down;
            justMovedDown = true;
        }

        else if (isEvenRow)
        {
            targetPos = new Vector3(Mathf.Floor(transform.position.x + 1f) + .5f, transform.position.y, 0);
            justMovedDown = false;


        }
            
        else if(!isEvenRow)
        {
            targetPos = new Vector3(Mathf.Ceil(transform.position.x - 1f) - .5f, transform.position.y, 0);

            justMovedDown = false;
        }
            

        isMoving = true;

    }

    void Update()
    {
        if (!isMoving) return;

        //Move Toward Taget
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        // Stop if close enough
        if (Vector3.Distance(transform.position, targetPos) < 0.00000000000000001f)
            isMoving = false;
    }

}
    
