using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float spawnerTopRow;
    private float lEdge;
    private float rEdge;
    private float fromCenterDistance;
    private bool nextSpawnRight;
    private int rowCount;
    private int currentRow;

    public GameObject wave;

    [Header ("Invader prefabs")]
    [SerializeField] private GameObject invader1;
    [SerializeField] private GameObject invader2;
    [SerializeField] private GameObject invader3;
    [SerializeField] private GameObject invader4;



        // Start is called before the first frame update
    void Start()
    {
        //Defines amount of rows
        rowCount = 2;
        
        //Defines boundaries
        spawnerTopRow = WorldBoundary.TopEdge - 1;
        lEdge = WorldBoundary.LeftEdge;
        rEdge = WorldBoundary.RightEdge;

        //Sets first spawn
        fromCenterDistance = .5f;
        nextSpawnRight = true;

        SpawnWave();   
    }

    private void SpawnWave()
    {
        //Makes the parent object 
        wave = new GameObject("Wave");
        //Loops through by row 
        for (int i = 0; i < rowCount; i++)
        {
            //Loops horizontal
            //We spawn right then left, then out by on from center
            //Stops when hits bounds
            for (float f = fromCenterDistance; fromCenterDistance < rEdge; f++)
            {
                if (nextSpawnRight)
                {
                    //Writes the spawn position to be off screen
                    var pos = new Vector3(fromCenterDistance, spawnerTopRow + 10 - i, 0f);

                    //Prepares invader object and rolls for invader type
                    GameObject invader = null;

                    //if(DifficultyScaleManager.difficultyValue = 1)

                    /*
                    int roll = UnityEngine.Random.Range(1, 5);

                    switch (roll)
                    {
                        case 1: invader = Instantiate(invader1, pos, Quaternion.identity); break;
                        case 2: invader = Instantiate(invader2, pos, Quaternion.identity); break;
                        case 3: invader = Instantiate(invader3, pos, Quaternion.identity); break;
                        case 4: invader = Instantiate(invader4, pos, Quaternion.identity); break;
                    }
                    */

                    //Assigns invader as child
                    invader.transform.SetParent(wave.transform);

                    // Assign a target position for the invader
                    FlyInScript movement = invader.GetComponent<FlyInScript>();
                    movement.TargetPosition = new Vector3(pos.x, spawnerTopRow - i, 0f);

                    //Makes sure we spawn to the left next
                    nextSpawnRight = false;
                }

                else
                {
                    //Mirrors the above but to the left 
                    var pos = new Vector3(-fromCenterDistance, spawnerTopRow + 10 - i, 0f);
                    GameObject invader = null;
                    int roll = UnityEngine.Random.Range(1, 5);

                    switch (roll)
                    {
                        case 1: invader = Instantiate(invader1, pos, Quaternion.identity); break;
                        case 2: invader = Instantiate(invader2, pos, Quaternion.identity); break;
                        case 3: invader = Instantiate(invader3, pos, Quaternion.identity); break;
                        case 4: invader = Instantiate(invader4, pos, Quaternion.identity); break;
                    }
                    invader.transform.SetParent(wave.transform);
                    FlyInScript movement = invader.GetComponent<FlyInScript>();
                    movement.TargetPosition = new Vector3(pos.x, spawnerTopRow - i, 0f);
                    nextSpawnRight = true;
                    fromCenterDistance++;
                }
            }
            //Resets from center so new row can spawn
            fromCenterDistance = .5f;
        }
    }
}
