using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private float splashingTime = 1f;
    public LevelList levelListDatabase;

    [HideInInspector]
    public int totalLevelIndex = 0;
    [HideInInspector]
    public int maxLevelIndex = 0;
    [HideInInspector]
    private Level currentLevel;
    [HideInInspector]
    public int currentLevelIndex = 0;

    private static LevelManager instance = null;
    // Game instance Singleton
    public static LevelManager sharedInstance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        maxLevelIndex = PlayerPreferencesManager.sharedInstance.GetMaximumUnlockedLevel();
        currentLevelIndex = PlayerPreferencesManager.sharedInstance.GetLastActiveLevel();
        totalLevelIndex = levelListDatabase.levelList.Count;
        Debug.Log("Player maximum level index is " + maxLevelIndex);
        Debug.Log("Player current level index is " + currentLevelIndex);
        Debug.Log("Game total level index is " + totalLevelIndex);

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public Level GetActiveLevel()
    {
        return levelListDatabase.levelList[currentLevelIndex-1];
    }

    private void setActiveLevel(int index)
    {
        currentLevelIndex = index;
        PlayerPreferencesManager.sharedInstance.SetLastActiveLevel(index);
        Debug.Log("New active scene is Level" + index);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(InitializeLevelsScene());
        }
    }

    private IEnumerator InitializeLevelsScene()
    {
        yield return new WaitForSeconds(splashingTime);
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLastActiveLevel(){
        LoadGameScene(currentLevelIndex);
    }

    public void LoadGameScene(int index)
    {
        setActiveLevel(index);
        SceneManager.LoadScene("Game");
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadLeaderBoardScene()
    {
        SceneManager.LoadScene("LeaderBoard");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadInfoScene()
    {
        SceneManager.LoadScene("Info");
    }
}
