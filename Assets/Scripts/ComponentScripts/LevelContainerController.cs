using System.Collections.Generic;
using LevelSystem;
using UnityEngine;

namespace ComponentScripts
{
	public class LevelContainerController : MonoBehaviour {

		private readonly int _lastActiveLevelPage;
		public GameObject LevelButton;

		private List<Level> _levelList;
		private int _pageNumber;
		private int _totalPageNumber;

		private const int LevelsPerPage = 18;

		// Use this for initialization
		private void Start ()
		{
			_levelList = LevelManager.SharedInstance.LevelListDatabase.LevelListDatabase;
			if (_levelList != null) _totalPageNumber = Mathf.FloorToInt(_levelList.Count / LevelsPerPage);
			_pageNumber = Mathf.FloorToInt(_lastActiveLevelPage / LevelsPerPage);
			RefreshLevelList();
		}

		private void RefreshLevelList()
		{	
			foreach(Transform child in transform) {
				Destroy(child.gameObject);
			}
			var startingIndex = LevelsPerPage * _pageNumber;
			var lastIndex = Mathf.Min(LevelsPerPage * (_pageNumber + 1), _levelList.Count);

			for (var i = startingIndex; i < lastIndex; i++)
			{
				var instanceButton = Instantiate(LevelButton);
				var newLevelButton = instanceButton.GetComponent<LevelButtonController>();
				var level = _levelList[i];
				newLevelButton.Level = level;
				newLevelButton.transform.SetParent(transform,false); 
			}			
		}

		public void MoveNextPage(){
			if (_pageNumber >= _totalPageNumber) return;
			_pageNumber += 1;
			RefreshLevelList();
		}

		public void MovePreviousPage (){
			if (_pageNumber <= 0) return;
			_pageNumber -= 1;
			RefreshLevelList();
		}
	}
}
