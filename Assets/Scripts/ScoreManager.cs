using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private Text scoreText;
    private int scoreValue;

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
        scoreText = GetComponent<Text>();
    }

    public void IncreaseScore(int score)
    {
        scoreValue += score;
        scoreText.text = "Score: " + scoreValue.ToString();
    }
}
