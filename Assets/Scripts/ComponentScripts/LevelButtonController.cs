using UnityEngine;
using UnityEngine.UI;
using System;
using LevelSystem;
using TMPro;


public class LevelButtonController : MonoBehaviour
{

    public Button levelNavigationButton;
    public Level level;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    // Use this for initialization
    private void Start()
    {	
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = level.index.ToString();
        levelNavigationButton.onClick.AddListener(InitializeLevel);
    }

    private void InitializeLevel()
    {
        // var levelIndex = level.levelIndex;
        if (level.status == LevelStatus.locked)
        {
            //TODO print warning: The level is locked
            print("level is locked");
        }
        else
        {
            LevelManager.SharedInstance.LoadGameScene(level.index);
        }
    }
}
