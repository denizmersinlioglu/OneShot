using LevelSystem;
using UnityEngine;

namespace Utility {
    public class PlayerPreferencesManager : MonoBehaviour {
        private const string maxUnlockLevelKey = "MaximumUnlockedLevelIndex";
        private const string lastActivateLevelKey = "LastActivateLevelIndex";

        static PlayerPreferencesManager() {
            SharedInstance = null;
        }

        public static PlayerPreferencesManager SharedInstance { get; private set; }

        private void Awake() {
            // if the singleton hasn't been initialized yet
            if (SharedInstance != null && SharedInstance != this) {
                Destroy(this.gameObject);
            }

            SharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void SetMaximumUnlockedLevel(int index) {
            if (index <= LevelManager.SharedInstance.totalLevelIndex) {
                PlayerPrefs.SetInt(maxUnlockLevelKey, index);
            } else {
                Debug.LogError("An error occured while getting max unlocked level");
            }
        }

        public int GetMaximumUnlockedLevel() {
            var index = PlayerPrefs.GetInt(maxUnlockLevelKey);
            return index;
        }

        public void SetLastActiveLevel(int index) {
            if (index <= LevelManager.SharedInstance.totalLevelIndex) {
                PlayerPrefs.SetInt(lastActivateLevelKey, index);
            } else {
                Debug.LogError("An error occured while getting last active level");
            }
        }

        public int GetLastActiveLevel() {
            var index = PlayerPrefs.GetInt(lastActivateLevelKey);
            return index;
        }

    }
}