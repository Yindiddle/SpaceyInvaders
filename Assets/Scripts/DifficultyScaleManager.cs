using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DifficultyScaleManager : MonoBehaviour
{
    void OnEnable() => GameManager.OnPulse += OnPulse;
    void OnDisable() => GameManager.OnPulse -= OnPulse;
    public static DifficultyScaleManager Instance { get; private set; }

    private Text difficultyText;
    public float difficultyValue;

    private int pulseCounter;

    [SerializeField] private float difficultyStepUpValue;
    [SerializeField] private int pulsesTilStepUp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        difficultyText = GetComponent<Text>();
    }

    void OnPulse()
    {
        pulseCounter++;
        if (pulseCounter >= pulsesTilStepUp)
        {
            IncreaseDifficulty();
            pulseCounter = 0;
        }
            
    }

    private void IncreaseDifficulty()
    {
        difficultyValue += difficultyStepUpValue;
        difficultyText.text = "!: " + difficultyValue.ToString();
    }
}
