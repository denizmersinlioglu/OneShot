using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class LevelButtonController : MonoBehaviour
{

    public Button levelNavigationButton;
    public Level level;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    // Use this for initialization
    void Start()
    {	
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = level.index.ToString();
        levelNavigationButton.onClick.AddListener(initializeLevel);
    }

    public void initializeLevel()
    {
        // var levelIndex = level.levelIndex;
        if (level.status == LevelStatus.locked)
        {
            //TODO print warning: The level is locked
            print("level is locked");
        }
        else
        {
            LevelManager.sharedInstance.LoadGameScene(level.index);
        }
    }
}
