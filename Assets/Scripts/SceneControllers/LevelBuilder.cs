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
        private LevelList _levelDatabase;

        [SerializeField]
        private int _levelNumber = 1;

        [SerializeField]
        private TextMeshProUGUI _headerLabel;

        [SerializeField]
        private Level _level;

        private void Awake()
        {
            _level = LevelManager.SharedInstance != null ? LevelManager.SharedInstance.GetActiveLevel() : _levelDatabase.LevelListDatabase[_levelNumber-1];
            Instantiate(_level.Structure, Vector3.zero, Quaternion.identity);
            _headerLabel.text = "Level " + _level.Index;
        }
    }
}
