﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
       for (int i = 0 ; i < levelCount; i++)
		{
			LevelStatus status = (LevelStatus)PlayerPrefs.GetInt("LevelStatus" + i);
			LevelControlType type = (LevelControlType)PlayerPrefs.GetInt("LevelType" + i);
			var newLevel = new Level(number:i, type: type, status: status);

			levelList.Add(newLevel);
		}

		levelPageCount = Mathf.FloorToInt(levelCount/levelCountForPage);
		if (levelCount % levelCountForPage == 0){
			levelPageCount -= 1;
		}
    }

	private float splashingTime = 4f;
	public static int levelCount = 77;
	public static int levelPageCount = 0;
	public static int levelCountForPage = 10;
	public static int lastActiveLevelPage = 0;
	public static List<Level> levelList = new List<Level>();
  	

	private IEnumerator InitializeLevelsScene()
    {
        yield return new WaitForSeconds(splashingTime);
        LoadLevelsScene();
    }

  	public static void LoadSameLevel(){
	  	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   	}

   	public static void LoadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}

	public static void LoadLevelsScene(){
		SceneManager.LoadScene("Levels");
	}

	public static void LoadInfoScene(){
		SceneManager.LoadScene("Levels");
	}

	public static void LoadAboutScene(){
		SceneManager.LoadScene("Levels");
	}

	public static void LoadSettingsScene(){
		SceneManager.LoadScene("Levels");
	}

	public static void LoadNextLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public static void StorePlayerLevelInformation(){
	}

	public static void deletePlayerLevelInformation()
	{	
		PlayerPrefs.DeleteAll();
	}

	void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0){
			StartCoroutine(InitializeLevelsScene());
		}
	}
		
}
