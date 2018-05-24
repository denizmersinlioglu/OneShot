using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class LevelButtonController : MonoBehaviour {

	public Button button;
	public Level level;

	public GameObject star1;
	public GameObject star2;
	public GameObject star3;

	// Use this for initialization
	void Start () {
		 gameObject.GetComponentInChildren<TextMeshProUGUI>().text = level.number.ToString();
//		button.enabled = !level.locked;
//		button.onClick.AddListener(() => loadLevels("Level" + level.levelNumber));
	}

	public void initializeLevel(){
		var levelIndex = level.number;
		if (level.status == LevelStatus.locked){
			//TODO print warning: The level is locked
			print("level is locked");
		}else{
			if (levelIndex == 0 || levelIndex == 34){
				LevelManager.LoadLevel("Level" + levelIndex);
				LevelManager.lastActiveLevelPage = levelIndex;
			}else{
				LevelManager.LoadLevel("Level" + 0);
			}
			
		}
		
	}
}
