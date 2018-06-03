using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNavigationController : MonoBehaviour {

	public void returnActiveLevelPage(){
		LevelManager.sharedInstance.LoadLevelsScene();
	}
}
