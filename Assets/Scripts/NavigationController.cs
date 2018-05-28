using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour {
    
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
 
	private const string levelsSceneName = "Levels";
    private LevelContainerController levelContainerController;

	void Start()
	{
		levelContainerController = GameObject.FindObjectOfType<LevelContainerController>();
	}


	void Update()
	{
		Swipe();
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
		levelContainerController.moveToMaximumLevel();
	}

	public void ChangeLayoutButtonPressed(){

	}

	public void InfoButtonPressed(){

	}

	public void Swipe()
	{
		if(Input.GetMouseButtonDown(0))
		{
			//save began touch 2d point
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}
		if(Input.GetMouseButtonUp(0))
		{
				//save ended touch 2d point
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		
				//create vector from the two points
			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
			
			//normalize the 2d vector
			currentSwipe.Normalize();
	
			//swipe upwards
			if(currentSwipe.y > 0 && (currentSwipe.x > -0.5f ||  currentSwipe.x < 0.5f))
			{
				Debug.Log("up swipe");
			}
			//swipe down
			if(currentSwipe.y < 0 && (currentSwipe.x > -0.5f || currentSwipe.x < 0.5f))
			{
				Debug.Log("down swipe");
			}
			//swipe left
			if(currentSwipe.x < -0.5 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
			{
				levelContainerController.moveNextPage();
			}
			//swipe right
			if(currentSwipe.x > 0.5 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
			{
				levelContainerController.movePreviousPage();
			}
		}
	}
	
}
