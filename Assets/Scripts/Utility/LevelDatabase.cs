using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelDatabase : ScriptableObject
{
    public List<Level> levelDatabase = new List<Level>();
}
 
[Serializable]
public class Level {

	public int number;
	public LevelControlType controlType;
	public LevelStatus status;
	
	public Level(int number, LevelControlType type, LevelStatus status) {
		this.number = number;
		this.controlType = type;
		this.status = status;
	}

	public Level(){
		number = 0;
		controlType = LevelControlType.none;
		status = LevelStatus.none;
	}
}

public enum LevelStatus{
	none = -3,
	locked = -1,
	incompleted = 0,
	completeOneStar = 1,
	completeTwoStars = 2,
	completeThreeStars = 3,
}

public enum LevelControlType{
	acc, launch, none
}
 


