using UnityEngine;
using System.Collections;
using LevelSystem;

[System.Serializable]                                                              //Representation of an Level.
public class Level
{
    // Identification
    public int Index = 0;                                                          //  Level index in the continer list.
    public string Name = "New level";                                              //  What the level will be called in the game.
    public string Hint = "Dummy Hint";                                             //  What the user will be see in the game if s/he watches the ads.

    // Level Assets
    public GameObject Structure = null;                                            //  Level environment constructed by prefabs and probs.

    // Progression Report
    public LevelStatus Status = LevelStatus.Locked;                                //  Hold the user progression for each level in the game
    public LevelCompletionStatus CompletionStatus = LevelCompletionStatus.None;    //  Hold user succes for each level. Its is none automatically if the level locked. 

    // Control Types
    public bool IsLaunchActive = false;                                            //  Determine the control type of the level
    public bool IsAccelerometerActive = false;                                     //  Determine the control type of the level
    public bool IsNavigationButtonsActive = false;                                 //  Determine the control type of the level

    // Hit Limits
    public int MaximumHitCount = 0;                                                //  Maximum hit number to destroy all the targets.
    public int ThreeStarHitLimit = 0;                                              //  Hit limits for succes rate of the user.
    public int TwoStarHitLimit = 0;                                                //  Hit limits for succes rate of the user.

    // Physical Conditions
    public bool IsGravityEnabled = false;                                          //  Switch for gravity in the level environment.
    public float GravityConstant = 0.0f;                                           //  Determine gravitational constant of the level environment.
    public bool IsBallPhysicsEnabled = false;                                      //  Switch for ball physics modifications. If the switch is off, ball will have default physics features.
    public float BallFrictionRate = 0.0f;                                          //  Determine friction for rigidbody of the ball. Its default value is 0.                                                   
    public float BallBounceRate = 0.0f;                                            //  Determine bounce rate for rigidbody of the ball. Its default value is 1.  
}
