using UnityEngine;
using UnityEngine.UI;



public class PauseManager : MonoBehaviour
{

    [SerializeField] private GameObject[] menuItems;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fxVolume;
    [SerializeField] private GameObject musicVolumeObj;
    [SerializeField] private GameObject UIManager;

    public static PauseManager Instance { get; private set; }
    public bool IsPaused { get; private set; } = false;

    private Text[] menuItemTexts;
    private int currentMenuItem;
    private float audioVolume;
    private float musicVolume;

    private GameObject wave;
    


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

        // Cache Text components for efficiency
        menuItemTexts = new Text[menuItems.Length];
        for (int i = 0; i < menuItems.Length; i++)
        {
            menuItemTexts[i] = menuItems[i].GetComponent<Text>();
        }

        //Defaults the color highlight
        RecolorMenuItems();
        TogglePauseMenu();
        TogglePauseMenu();
        audioVolume = 1;
        musicVolume = 1;
    }

    void Update()
    {
        if (IsPaused)
        {
            //Allows Moving up and down menu items
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
            {
                currentMenuItem += Input.GetKeyDown(KeyCode.S) ? 1 : -1;
                currentMenuItem = (currentMenuItem + menuItems.Length) % menuItems.Length;
                RecolorMenuItems();
            }

            //Resumes Game
            if (currentMenuItem == 0 && Input.GetKeyDown(KeyCode.Return))
                ResumeGame();

            //Fx Volume edits
            if (currentMenuItem == 1)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    UpdateVolume();
                }
            }

            //Music Volume
            if (currentMenuItem == 2)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    UpdateMusicVolume();
                }
            }

            //Quits Game
            if (currentMenuItem == 3 && Input.GetKeyDown(KeyCode.Return))
            {
                Application.Quit();
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
        }         
      
    }

    public void TogglePauseMenu()
    {
        if (IsPaused)
        {
            ResumeGame();
        }
            
        else
        {
            PauseGame();
        }
    }

    private void RecolorMenuItems()
    {
        for (int i = 0; i < menuItemTexts.Length; i++)
        {
            menuItemTexts[i].color = (i == currentMenuItem &&
                ColorUtility.TryParseHtmlString("#FFC200", out var highlightColor))
                ? highlightColor
                : Color.white;
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        IsPaused = false;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        IsPaused = true;
        wave = FindObject("Wave");
    }

    private void UpdateVolume()
    {
        //Increments by 5
        audioVolume += Input.GetKeyDown(KeyCode.D) ? .05f : -.05f;

        //Sets Min/Max Volume
        if (audioVolume > 1)
            audioVolume = 1;
        if (audioVolume < 0)
            audioVolume = 0;

        //Updates the text in the menu
        fxVolume.GetComponentInChildren<Text>().text = "Fx Volume: " + Mathf.Round(audioVolume * 100) + "%";

        //Updates the player object volume
        player.GetComponent<AudioSource>().volume = audioVolume;

        //Updates each invader's volume
        foreach(Transform invader in wave.transform)
        {
            invader.GetComponent<AudioSource>().volume = audioVolume;
        }
    }

    private void UpdateMusicVolume()
    {
        //Increments/Decrements by 5
        musicVolume += Input.GetKeyDown(KeyCode.D) ? .05f : -.05f;

        //Sets Min/Max Volume
        if (musicVolume > 1)
            musicVolume = 1;
        if (musicVolume < 0)
            musicVolume = 0;

        //Updates the text in the menu
        musicVolumeObj.GetComponentInChildren<Text>().text = "Music Volume: " + Mathf.Round(musicVolume * 100) + "%";

        //Update Volume
        UIManager.GetComponent<AudioSource>().volume = musicVolume;
    }

    public GameObject FindObject(string waveName)
    {
        GameObject waveObject = GameObject.Find(waveName);
        if (waveObject != null)
        {
            return waveObject;
        }
        else
        {
            Debug.LogError($"Wave '{waveName}' not found in the scene!");
            return null;
        }
    }
}
