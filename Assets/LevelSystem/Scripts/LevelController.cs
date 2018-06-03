using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    // A moderator will give the level of this level container.
    // The aim of this class is building level according the acquired data from moderator.

    // Get control type from level instance - Create game mechanics according to control types.
    // Get level hint from level instance - Edit the hint text according to hint string.
    // Get hit count from level instance - Control win/lose state of the level.
    // Get success limit numbers from level instance - Control user success rate according to twoStarsHit# and threeStarsHit#.

    // When user complete the level - Update LevelDatabase asset according to users success.

    public Level level;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        level = LevelManager.sharedInstance.GetActiveLevel();
		GameObject structure = (GameObject)GameObject.Instantiate(level.structure, Vector3.zero, Quaternion.identity);
    }
}
