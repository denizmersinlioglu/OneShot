using UnityEngine;
using System.Collections;

[System.Serializable]                                                                   //  Our Representation of an Level.
public class Level
{
    public int levelIndex = 0;                                                          //  Level index in the continer list.
    public string levelName = "New level";                                              //  What the level will be called in the game.
    public string levelHint = "Dummy Hint";                                             //  What the user will be see in the game if s/he watches the ads.
    public int maximumHitCount = 0;                                                     //  Maximum hit number to destroy all the targets.
    public int threeStarHitLimit = 0;                                                   //  Hit limits for succes rate of the user.
    public int twoStarHitLimit = 0;                                                     //  Hit limits for succes rate of the user.
    public LevelStatus levelStatus = LevelStatus.locked;                                //  How we hold the user progression for each level in the game
    public LevelCompletionStatus levelCompletionStatus = LevelCompletionStatus.none;    //  How we hold user succes for each level. Its is none automatically if the level locked. 
    public bool isLaunchActive = false;                                                 //  How we determine the control type of the level
    public bool isAccelerometerActive = false;                                          //  How we determine the control type of the level
    public bool isNavigationButtonsActive = false;                                      //  How we determine the control type of the level
}
