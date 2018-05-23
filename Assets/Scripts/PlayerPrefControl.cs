using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefControl : MonoBehaviour {

	public static string MASTER_VOLUME_KEY = "masterVolume";
	public static string UNLOCK_LEVEL_KEY = "unlockedLevel";
	
//	public static string LEVEL_DIFFICULTY_KEY = "levelDifficulty";
//	public static string SCREEN_BRIGHTNESS_KEY = "screenBrightness";

#region Master Volume Region

	public static void SetMasterVolume(int volume){
		if(volume ==0 || volume == 1){
		PlayerPrefs.SetInt(MASTER_VOLUME_KEY,volume);
		} else{
		Debug.LogError("Invalid master volume value");} 
	}
	public static int GetMasterVolume(){
		return PlayerPrefs.GetInt(MASTER_VOLUME_KEY);
	}
	#endregion

#region Unlock Level Region
	public static void UnlockLevel(int level){
		if(level<= SceneManager.sceneCountInBuildSettings - 1 ){
			PlayerPrefs.SetInt(UNLOCK_LEVEL_KEY + level.ToString(), 1);
		
		}else{Debug.LogError("Demanded level doesn't exist");}
	}
	
	public static bool IsLevelUnlocked(int level){
		int levelValue = PlayerPrefs.GetInt(UNLOCK_LEVEL_KEY + level.ToString());	
		bool isLevelUnlock = (levelValue==1);
			
		if(level<= SceneManager.sceneCountInBuildSettings -1 ){
			return isLevelUnlock;
			
		}else{
			Debug.LogError("Demanded level doesn't exist");
			return false;}
	}
#endregion

#region Other Prefences Region
//	public static void SetLevelDifficulty(float difficulty){
//		if (difficulty >=0 && difficulty <= 6){
//		PlayerPrefs.SetFloat(LEVEL_DIFFICULTY_KEY,difficulty);
//		}else{
//		Debug.LogError("Invalid level difficulty value");
//		}
//	}
//	public static float GetLevelDifficulty(){
//		return PlayerPrefs.GetFloat(LEVEL_DIFFICULTY_KEY);
//	}
//
//	public static void SetScreenBrightness(float brightness){
//		if(brightness>=0 && brightness <= 1){
//			PlayerPrefs.SetFloat(SCREEN_BRIGHTNESS_KEY,brightness);
//		} else{
//			Debug.LogError("Invalid screen brightness value");
//		}
//	}
//	public static float GetScreenBrightness(){
//		return PlayerPrefs.GetFloat(SCREEN_BRIGHTNESS_KEY);
//	}
//

#endregion

}
