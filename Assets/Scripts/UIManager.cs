using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private PauseManager pauseManager;


    private void Start()
    {
        pauseManager = pauseMenu.GetComponent<PauseManager>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        pauseManager.TogglePauseMenu();
    }
}
