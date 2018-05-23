using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelButtonController : MonoBehaviour {

	public Button button;
	public Level level;

	public GameObject star1;
	public GameObject star2;
	public GameObject star3;

	// Use this for initialization
	void Start () {
		 gameObject.GetComponentInChildren<TextMeshProUGUI>().text = level.levelNumber.ToString();
//		button.enabled = !level.locked;
//		button.onClick.AddListener(() => loadLevels("Level" + level.levelNumber));
	}

	void loadLevels(string value)
	{
		SceneManager.LoadScene(value);
	}

	public void initializeLevel(){
		var levelNum = level.levelNumber;
		if (levelNum == 2){
			SceneManager.LoadScene("Level" + 2);
		}else{
			SceneManager.LoadScene("Level" + 1);
		}
		
	}
}
