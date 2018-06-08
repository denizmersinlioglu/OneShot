using UnityEngine;
using UnityEngine.UI;
using System;
using LevelSystem;
using TMPro;


public class LevelButtonController : MonoBehaviour
{

    public Button LevelNavigationButton;
    public Level Level;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    // Use this for initialization
    private void Start()
    {	
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Level.Index.ToString();
        LevelNavigationButton.onClick.AddListener(InitializeLevel);
    }

    private void InitializeLevel()
    {
        // var levelIndex = level.levelIndex;
        if (Level.Status == LevelStatus.Locked)
        {
            //TODO print warning: The level is locked
            print("level is locked");
        }
        else
        {
            LevelManager.SharedInstance.LoadGameScene(Level.Index);
        }
    }
}
