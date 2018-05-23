using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class LevelSceneController : MonoBehaviour {

	[SerializeField] private int maxLevelNumber;
	public int levelPerPage;
	
	public static string lastLevelScene;
	public GameObject levelButton;
	public Transform levelContainerPanel;
	public static List<Level> levelList;


	void Start ()
	{
		lastLevelScene = SceneManager.GetActiveScene().name;
		PlayerPrefs.SetString("LoadedLevel",lastLevelScene);
		levelList = populateLevelList(PlayerPrefs.GetInt("LevelsPageNumber"));
		fillLevelList();	
	}

	private List<Level> populateLevelList(int pageNumber){
		var output = new List<Level>();
		for (int i = ((pageNumber -1) * levelPerPage) + 1 ; i <= (pageNumber * levelPerPage) ; i++)
		{
			var newLevel = new Level();
			newLevel.levelNumber = i;
			newLevel.locked = true;
			output.Add(newLevel);
		}
		return output;
	} 

	void fillLevelList()
	{
		foreach(var level in levelList)
		{
			GameObject instanceButton = Instantiate(levelButton) as GameObject;
			LevelButtonController newLevelButton =   instanceButton.GetComponent<LevelButtonController>();

			if(PlayerPrefs.GetInt("Level" + level.levelNumber) == 1)
			{
				level.locked = false;
			}

			newLevelButton.level = level;
			newLevelButton.transform.SetParent(levelContainerPanel,false); 
		}
			savePlayerPrefences();	
	}

	static void savePlayerPrefences()
	{
		if(PlayerPrefs.HasKey("Level1"))
		{
			return;
		}else{
			foreach(Level level in levelList)
				{
					PlayerPrefs.SetInt("Level" + level.levelNumber, level.locked ? 0 : 1);
				}
			}
	}

	public void deleteAll()
	{	
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	
}
