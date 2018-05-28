using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainerController : MonoBehaviour {

	public GameObject levelButton;
	private int pageNumber = 0;
	
	// Use this for initialization
	void Start () {
		pageNumber = Mathf.FloorToInt(LevelManager.lastActiveLevelPage / LevelManager.levelCountForPage);
		FillLevelList();
	}

	void FillLevelList()
	{	
		foreach(Transform child in transform) {
    		Destroy(child.gameObject);
		}
		var startingIndex = LevelManager.levelCountForPage * pageNumber;
		var lastIndex = Mathf.Min(LevelManager.levelCountForPage * (pageNumber + 1), LevelManager.levelCount);
		for (int i = startingIndex; i < lastIndex; i++)
		{
			GameObject instanceButton = Instantiate(levelButton) as GameObject;
			LevelButtonController newLevelButton = instanceButton.GetComponent<LevelButtonController>();
			var level = LevelManager.levelList[i];
			newLevelButton.level = level;
			newLevelButton.transform.SetParent(this.transform,false); 
		}			
	}

	public int moveNextPage(){
		if (pageNumber < LevelManager.levelPageCount){
			pageNumber += 1;
			FillLevelList();
		}
		return pageNumber;
	}

	public int movePreviousPage (){
		if (pageNumber > 0){
			pageNumber -= 1;
			FillLevelList();
		}
		return pageNumber;
	}

	public int moveToMaximumLevel(){
		int maximumLevelNumber = 0;
		foreach(Level level in LevelManager.levelList){
			if (level.status != LevelStatus.locked){
				maximumLevelNumber = Mathf.Max(level.number, maximumLevelNumber);
			}
		}
		pageNumber = Mathf.FloorToInt(maximumLevelNumber / LevelManager.levelCountForPage);
		FillLevelList();
		return maximumLevelNumber;
	}
}
