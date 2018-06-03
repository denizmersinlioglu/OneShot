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
 
    private LevelContainerController levelContainerController;

	void Start()
	{
		levelContainerController = GameObject.FindObjectOfType<LevelContainerController>();
	}

	// Mark: - Button Actions
	public void BackToLevelMenu(){
		LevelManager.sharedInstance.LoadLevelsScene();
	}

	public void NextPageButtonPressed(){
		levelContainerController.moveNextPage();
	}

	public void PreviousPageButtonPressed(){
		levelContainerController.movePreviousPage();
	}

	public void HomePageButtonPressed(){
		LevelManager.sharedInstance.LoadMainMenu();
	}

	public void ChangeLayoutButtonPressed(){

	}

	public void InfoButtonPressed(){

	}
}
