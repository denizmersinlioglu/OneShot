using LevelSystem;
using UnityEngine;

namespace Utility {
	public class NavigationController : MonoBehaviour {

		//TODO swipe control might be added

		// Mark: - Button Actions
		public void LoadLevelsScene() {
			LevelManager.LoadLevelsScene();
		}

		public void LoadLastActiveLevel() {
			LevelManager.SharedInstance.LoadLastActiveLevel();
		}

		public void HomePageButtonPressed() {
			LevelManager.LoadMainMenu();
		}

		public void ChangeLayoutButtonPressed() {
			//TODO Change the layout of the levels scene
		}

		public void InfoButtonPressed() {
			//TO DO Give Info About Levels
		}
	}
}