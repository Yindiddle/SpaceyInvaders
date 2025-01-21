using UnityEngine;
using System;               
using System.Collections;  

public class GameManager : MonoBehaviour
{
    public static event Action OnPulse;

    [SerializeField] float pulseInterval = 2f;
    
    private int pulseCounter;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            //Generates pulse on interval
            yield return new WaitForSeconds(pulseInterval);
            OnPulse?.Invoke();
        }
    }
}
