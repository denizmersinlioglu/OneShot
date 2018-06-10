using System.Collections.Generic;
using LevelSystem;
using UnityEngine;

namespace ComponentScripts
{
	public class LevelContainerController : MonoBehaviour {

		private readonly int lastActiveLevelPage;
		public GameObject levelButton;

		private List<Level> levelList;
		private int pageNumber;
		private int totalPageNumber;

		private const int levelsPerPage = 18;

		// Use this for initialization
		private void Start ()
		{
			levelList = LevelManager.SharedInstance.levelListDatabase.levelListDatabase;
			if (levelList != null) totalPageNumber = Mathf.FloorToInt(levelList.Count / levelsPerPage);
			pageNumber = Mathf.FloorToInt(lastActiveLevelPage / levelsPerPage);
			RefreshLevelList();
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
