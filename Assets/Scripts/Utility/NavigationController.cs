using UnityEngine;

public class NavigationController : MonoBehaviour {

    //TODO swipe control might be added
	
	// Mark: - Button Actions
	public void LoadLevelsScene(){
		LevelManager.sharedInstance.LoadLevelsScene();
	}

	public void LoadLastActiveLevel(){
		LevelManager.sharedInstance.LoadLastActiveLevel();
	}

	public void HomePageButtonPressed(){
		LevelManager.sharedInstance.LoadMainMenu();
	}

	public void ChangeLayoutButtonPressed(){
		//TODO Change the layout of the levels scene
	}

	public void InfoButtonPressed(){
		//TO DO Give Info About Levels
	}
}
