using UnityEngine;

public class PlayerPreferencesManager : MonoBehaviour
{
    private readonly string MAX_UNLOCK_LEVEL_KEY = "MaximumUnlockedLevelIndex";
	private readonly string LAST_ACTIVATE_LEVEL_KEY = "LastActivateLevelIndex";

    private static PlayerPreferencesManager instance = null;

    public static PlayerPreferencesManager sharedInstance
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

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetMaximumUnlockedLevel(int index)
    {
        if (index <= LevelManager.sharedInstance.totalLevelIndex)
        {
            PlayerPrefs.SetInt(MAX_UNLOCK_LEVEL_KEY, index);
        }
        else
        { 
			Debug.LogError("An error occured while getting max unlocked level"); 
		}
    }

    public int GetMaximumUnlockedLevel()
    {	
		int index =  PlayerPrefs.GetInt(MAX_UNLOCK_LEVEL_KEY); 
        return index;
    }

	public void SetLastActiveLevel(int index)
    {
        if (index <= LevelManager.sharedInstance.totalLevelIndex)
        {
            PlayerPrefs.SetInt(LAST_ACTIVATE_LEVEL_KEY, index);
        }
        else
        { 
			Debug.LogError("An error occured while getting last active level"); 
		}
    }

    public int GetLastActiveLevel()
    {	
		int index =  PlayerPrefs.GetInt(LAST_ACTIVATE_LEVEL_KEY);
        return index;
    }

}
