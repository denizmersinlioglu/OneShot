using LevelSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LevelSystem;


namespace SceneControllers
{
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
        private Level level;

        private void Awake()
        {
            level = LevelManager.SharedInstance != null ? LevelManager.SharedInstance.GetActiveLevel() : levelDatabase.levelListDatabase[levelNumber-1];
            Instantiate(level.structure, Vector3.zero, Quaternion.identity);
           
        }
    }
}
