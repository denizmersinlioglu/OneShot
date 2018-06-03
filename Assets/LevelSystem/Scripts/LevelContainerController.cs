using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainerController : MonoBehaviour {

	public LevelList levelListAsset;
	public GameObject levelButton;

	private List<Level> levelList;
	private int pageNumber = 0;
	private int totalPageNumber = 0;
	private int lastActiveLevelPage = 0;
	private int levelsPerPage = 10;
	// Use this for initialization
	void Start () {
		levelList = levelListAsset.levelList;
		totalPageNumber = Mathf.FloorToInt(levelList.Count / levelsPerPage);
		pageNumber = Mathf.FloorToInt(lastActiveLevelPage / levelsPerPage);
		RefreshLevelList();
	}

	void RefreshLevelList()
	{	
		foreach(Transform child in transform) {
    		Destroy(child.gameObject);
		}
		var startingIndex = levelsPerPage * pageNumber;
		var lastIndex = Mathf.Min(levelsPerPage * (pageNumber + 1), levelList.Count);
		
		for (int i = startingIndex; i < lastIndex; i++)
		{
			GameObject instanceButton = Instantiate(levelButton) as GameObject;
			LevelButtonController newLevelButton = instanceButton.GetComponent<LevelButtonController>();
			var level = levelList[i];
			newLevelButton.level = level;
			newLevelButton.transform.SetParent(this.transform,false); 
		}			
	}

	public int moveNextPage(){
		if (pageNumber < totalPageNumber){
			pageNumber += 1;
			RefreshLevelList();
		}
		return pageNumber;
	}

	public int movePreviousPage (){
		if (pageNumber > 0){
			pageNumber -= 1;
			RefreshLevelList();
		}
		return pageNumber;
	}

	public int moveToMaximumLevel(){
		int maximumLevelNumber = 0;
		foreach(Level level in levelList){
			if (level.levelStatus != LevelStatus.locked){
				maximumLevelNumber = Mathf.Max(level.levelIndex, maximumLevelNumber);
			}
		}
		pageNumber = Mathf.FloorToInt(maximumLevelNumber / levelsPerPage);
		RefreshLevelList();
		return maximumLevelNumber;
	}
}
