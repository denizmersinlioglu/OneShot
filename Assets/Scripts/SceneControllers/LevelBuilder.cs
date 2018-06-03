using UnityEngine;
using TMPro;

public class LevelBuilder : MonoBehaviour
{

    // A moderator will give the level of this level container. DONE
    // The aim of this class is building level according the acquired data from moderator.

    // Get control type from level instance - Create game mechanics according to control types.
    // Get level hint from level instance - Edit the hint text according to hint string.
    // Get hit count from level instance - Control win/lose state of the level.
    // Get success limit numbers from level instance - Control user success rate according to twoStarsHit# and threeStarsHit#.

    // When user complete the level - Update LevelDatabase asset according to users success.
    [SerializeField]
    private LevelList levelDatabase;

    [SerializeField]
    private int levelNumber = 1;

	[SerializeField]
	private TextMeshProUGUI headerLabel;

	[SerializeField]
    private Level level;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(LevelManager.sharedInstance != null)
        {
            level = LevelManager.sharedInstance.GetActiveLevel();
        }else
        {
            level = levelDatabase.levelList[levelNumber-1];
        }
		GameObject structure = (GameObject)GameObject.Instantiate(level.structure, Vector3.zero, Quaternion.identity);
		headerLabel.text = "Level " + level.index;
    }
}
