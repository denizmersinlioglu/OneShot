using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] LevelMusic;
	
	public AudioSource music;
	
	private void OnEnable() {

	}
	void Awake(){
		// DontDestroyOnLoad(gameObject);
	}
	
	void Start () {
	
	}
	
	public void ChangeVolume(float volume){
		
	}
}
