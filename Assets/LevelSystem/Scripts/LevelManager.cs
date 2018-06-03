using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private float splashingTime = 4f;
    public LevelList levelListDatabase;

    [HideInInspector]
    public int currentLevelIndex = 0;
    [HideInInspector]
    public int totalLevelIndex = 0;
    [HideInInspector]
    public int maxLevelIndex = 0;

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

        instance = this;
        DontDestroyOnLoad(this.gameObject);
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

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene("LEvel" + index);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadInfoScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadAboutScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void StorePlayerLevelInformation()
    {
    }

    public void deletePlayerLevelInformation()
    {
        PlayerPrefs.DeleteAll();
    }

}
