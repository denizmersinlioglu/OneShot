using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour {

    //TODO swipe control might be added
	
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
 
	private const string levelsSceneName = "Levels";
    private LevelContainerController levelContainerController;

	void Start()
	{
		levelContainerController = GameObject.FindObjectOfType<LevelContainerController>();
	}

	// Mark: - Button Actions
	public void BackToLevelMenu(){
		LevelManager.LoadLevel(levelsSceneName);
	}

	public void NextPageButtonPressed(){
		levelContainerController.moveNextPage();
	}

	public void PreviousPageButtonPressed(){
		levelContainerController.movePreviousPage();
	}

	public void HomePageButtonPressed(){
		LevelManager.LoadMainMenu();
	}

	public void ChangeLayoutButtonPressed(){

	}

	public void InfoButtonPressed(){

	}
}
