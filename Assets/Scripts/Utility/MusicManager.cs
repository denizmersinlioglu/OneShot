using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] LevelMusic;
	
	public AudioSource music;
	
	void Awake(){
		DontDestroyOnLoad(gameObject);
	}
	
	void Start () {
		
		music = this.GetComponent<AudioSource>();
		music.clip= LevelMusic[0];
		music.Play();
	}
	
	void OnLevelWasLoaded(int level){
		//music.volume = PlayerPrefControl.GetMasterVolume();
		if(LevelMusic[level] && SceneManager.GetActiveScene().buildIndex != 0){
		music.clip = LevelMusic[level];
		if(level==4){music.loop=false;}
		else music.loop = true;
		music.Play();
	}
	}
	
	public void ChangeVolume(float volume){
		music.volume = volume;
	}
}
