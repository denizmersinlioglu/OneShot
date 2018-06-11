using System.Collections.Generic;
using LevelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace ComponentScripts
{
	public class LevelContainerController : MonoBehaviour
	{
		private const int col = 3;
		private const int row = 7;
		private Vector2 resolution;
		
		private readonly int lastActiveLevelPage;
		public GameObject levelButton;

		private List<Level> levelList;
		private int pageNumber;
		private int totalPageNumber;

		private const int levelsPerPage = 21;

		// Use this for initialization
		private void Start ()
		{
			levelList = LevelManager.SharedInstance.levelListDatabase.levelListDatabase;
			if (levelList != null) totalPageNumber = Mathf.FloorToInt(levelList.Count / levelsPerPage);
			pageNumber = Mathf.FloorToInt(lastActiveLevelPage / levelsPerPage);
			RefreshLevelList();

			updateGridResolution();
			resolution = new Vector2(Screen.width, Screen.height);
		}

		private void Update()
		{
			if (resolution.x == Screen.width && resolution.y == Screen.height) return;
			updateGridResolution();
			resolution.x = Screen.width;
			resolution.y = Screen.height;
		}

		private void updateGridResolution()
		{
			var parent = GetComponent<RectTransform>();
			var grid = GetComponent<GridLayoutGroup>();
			grid.cellSize = new Vector2(parent.rect.width/(col+1) , parent.rect.height/(row + 2));
		}

		private void RefreshLevelList()
		{	
			foreach(Transform child in transform) {
				Destroy(child.gameObject);
			}
			var startingIndex = levelsPerPage * pageNumber;
			var lastIndex = Mathf.Min(levelsPerPage * (pageNumber + 1), levelList.Count);

			for (var i = startingIndex; i < lastIndex; i++)
			{
				var instanceButton = Instantiate(levelButton);
				var newLevelButton = instanceButton.GetComponent<LevelButtonController>();
				var level = levelList[i];
				newLevelButton.level = level;
				newLevelButton.transform.SetParent(transform,false); 
			}			
		}

		public void MoveNextPage(){
			if (pageNumber >= totalPageNumber) return;
			pageNumber += 1;
			RefreshLevelList();
		}

		public void MovePreviousPage (){
			if (pageNumber <= 0) return;
			pageNumber -= 1;
			RefreshLevelList();
		}
	}
}
