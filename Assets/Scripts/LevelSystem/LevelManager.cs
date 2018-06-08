using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private const float SplashingTime = 2f;
        public LevelList LevelListDatabase;

        [HideInInspector]
        public int TotalLevelIndex;
        [HideInInspector]
        public int MaxLevelIndex;
        [HideInInspector]
        private Level _currentLevel;
        [HideInInspector]
        public int CurrentLevelIndex;

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

            MaxLevelIndex = PlayerPreferencesManager.sharedInstance.GetMaximumUnlockedLevel();
            CurrentLevelIndex = PlayerPreferencesManager.sharedInstance.GetLastActiveLevel();
            TotalLevelIndex = LevelListDatabase.LevelListDatabase.Count;
            Debug.Log("Player maximum level index is " + MaxLevelIndex);
            Debug.Log("Player current level index is " + CurrentLevelIndex);
            Debug.Log("Game total level index is " + TotalLevelIndex);

            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(InitializeLevelsScene());
        }

        public Level GetActiveLevel()
        {
            return LevelListDatabase.LevelListDatabase[CurrentLevelIndex-1];
        }

        private void SetActiveLevel(int index)
        {
            CurrentLevelIndex = index;
            PlayerPreferencesManager.sharedInstance.SetLastActiveLevel(index);
            Debug.Log("New active scene is Level" + index);
        }

        private IEnumerator InitializeLevelsScene()
        {
            yield return new WaitForSeconds(SplashingTime);
            LoadMainMenu();
        }

        public static void LoadMainMenu()
        {
            Initiate.Fade("MainMenu", Color.black, 2f);
        }

        public void LoadLastActiveLevel(){
            LoadGameScene(CurrentLevelIndex);
        }

        public void LoadGameScene(int index)
        {
            SetActiveLevel(index);
            Initiate.Fade("Game", Color.black, 3f);
            // SceneManager.LoadScene("Game");
        }

        public static void LoadLevelsScene()
        {
            Initiate.Fade("Levels", Color.black, 3f);
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
