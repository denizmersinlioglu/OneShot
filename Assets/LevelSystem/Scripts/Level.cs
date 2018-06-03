using UnityEngine;
using System.Collections;

[System.Serializable]                                                                   //  Our Representation of an Level.
public class Level
{
    // Identification
    public int levelIndex = 0;                                                          //  Level index in the continer list.
    public string levelName = "New level";                                              //  What the level will be called in the game.
    public string levelHint = "Dummy Hint";                                             //  What the user will be see in the game if s/he watches the ads.

    // Progression Report
    public LevelStatus levelStatus = LevelStatus.locked;                                //  Hold the user progression for each level in the game
    public LevelCompletionStatus levelCompletionStatus = LevelCompletionStatus.none;    //  Hold user succes for each level. Its is none automatically if the level locked. 

    // Control Types
    public bool isLaunchActive = false;                                                 //  Determine the control type of the level
    public bool isAccelerometerActive = false;                                          //  Determine the control type of the level
    public bool isNavigationButtonsActive = false;                                      //  Determine the control type of the level

    // Hit Limits
    public int maximumHitCount = 0;                                                     //  Maximum hit number to destroy all the targets.
    public int threeStarHitLimit = 0;                                                   //  Hit limits for succes rate of the user.
    public int twoStarHitLimit = 0;                                                     //  Hit limits for succes rate of the user.

    // Physical Conditions
    public bool isGravityEnabled = false;                                               //  Switch for gravity in the level environment.
    public float gravityConstant = 0.0f;                                                //  Determine gravitational constant of the level environment.
    public bool isBallPhysicsEnabled = false;                                           //  Switch for ball physics modifications. If the switch is off, ball will have default physics features.
    public float ballFrictionRate = 0.0f;                                               //  Determine friction for rigidbody of the ball. Its default value is 0.                                                   
    public float ballBounceRate = 0.0f;                                                 //  Determine bounce rate for rigidbody of the ball. Its default value is 1.  
}
