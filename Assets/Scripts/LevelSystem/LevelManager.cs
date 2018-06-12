using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private const float splashingTime = 2f;
        public LevelList levelListDatabase;

        [HideInInspector]
        public int totalLevelIndex;
        [HideInInspector]
        public int maxLevelIndex;
        [HideInInspector]
        private Level currentLevel;
        [HideInInspector]
        private int currentLevelIndex;

        static LevelManager()
        {
            SharedInstance = null;
        }

        // Game instance Singleton
        public static LevelManager SharedInstance { get; private set; }

        private void Awake()
        {
            // if the singleton hasn't been initialized yet
            if (SharedInstance != null && SharedInstance != this)
            {
                Destroy(gameObject);
            }

            maxLevelIndex = PlayerPreferencesManager.SharedInstance.GetMaximumUnlockedLevel();
            currentLevelIndex = PlayerPreferencesManager.SharedInstance.GetLastActiveLevel();
            totalLevelIndex = levelListDatabase.levelListDatabase.Count;
            Debug.Log("Player maximum level index is " + maxLevelIndex);
            Debug.Log("Player current level index is " + currentLevelIndex);
            Debug.Log("Game total level index is " + totalLevelIndex);

            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(InitializeLevelsScene());
        }

        public Level GetActiveLevel()
        {
            return levelListDatabase.levelListDatabase[currentLevelIndex-1];
        }

        private void SetPlayersActiveLevel(int index)
        {
            currentLevelIndex = index;
            PlayerPreferencesManager.SharedInstance.SetLastActiveLevel(index);
            Debug.Log("New active scene is Level" + index);
        }

        private static IEnumerator InitializeLevelsScene()
        {
            yield return new WaitForSeconds(splashingTime);
            LoadMainMenu();
        }

        public static void LoadMainMenu()
        {
            Initiate.Fade("MainMenu", Color.black, 3f);
        }

        public void LoadLastActiveLevel(){
            LoadGameScene(currentLevelIndex);
        }

        public void LoadGameScene(int index)
        {
            if (levelListDatabase.levelListDatabase[index-1].status == LevelStatus.locked) return;
            SetPlayersActiveLevel(index);
            Initiate.Fade("Game", Color.black, 4f);
            // SceneManager.LoadScene("Game");
        }

        public void UnlockLevel(int index)
        {
            levelListDatabase.levelListDatabase[index-1].status = LevelStatus.unlocked;
        }

        public static void LoadLevelsScene()
        {
            Initiate.Fade("Levels", Color.black, 4f);
            // SceneManager.LoadScene("Levels");
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
}
