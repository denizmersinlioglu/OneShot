using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private float splashingTime = 4f;

	public void LoadAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void LoadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}
	
	public void QuitRequest()
	{
		Application.Quit ();
	}

	public void LoadLevelsScene(){
			SceneManager.LoadScene(1);
	}
	
	private void LoadStartScene(){
		Invoke("LoadLevelsScene",splashingTime);
	}

	public void LoadNextLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0){
			LoadStartScene();
		}
	}
}
