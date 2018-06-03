using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	private float splashingTime = 4f;

	void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0){
			StartCoroutine(InitializeLevelsScene());
		}
	}

    private IEnumerator InitializeLevelsScene()
    {
        yield return new WaitForSeconds(splashingTime);
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadSameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void LoadLevelsScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public static void LoadInfoScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public static void LoadAboutScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public static void LoadSettingsScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void StorePlayerLevelInformation()
    {
    }

    public static void deletePlayerLevelInformation()
    {
        PlayerPrefs.DeleteAll();
    }

}
