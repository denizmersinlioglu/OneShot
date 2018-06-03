using UnityEngine;
using TMPro;

public class LevelBuilder : MonoBehaviour
{

    /* 
        A moderator will give the level of this level container.(DONE)
        The aim of this class is building level construction according the acquired data (level structure) from moderator class.

        Get the index of the level - Rename the header of the game. (DONE)
        Get level hint from level instance - Edit the hint text according to hint string.
        Get control type booleans from level instance - Create game control mechanics according to control types.
        Get physics features from level instance - Assign these properties to target component.
        Get ball physics features from level instance - You may need to create new physics 2D material. Ball is in the level structure prefab.
    */
    
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
