using LevelSystem;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private LevelList levelDatabase;
    [SerializeField] private int levelNumber = 1;
    [SerializeField] private Text hitLabel;
    [SerializeField] private Level level;
    private int hitCount = 1000;
    private int targetCount = 1000;

    private BaseProjectile Ball {
        get { return FindObjectOfType<BaseProjectile>(); }
    }

    private BaseTarget[] Targets {
        get { return FindObjectsOfType<BaseTarget>(); }
    }

    private void Awake() {
        level = LevelManager.SharedInstance != null ? LevelManager.SharedInstance.GetActiveLevel() : levelDatabase.levelListDatabase[levelNumber - 1];
        targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
        hitCount = level.maximumHitCount;
    }

    private void Start() {
        Ball.OnBallCollided += UpdateHitCount;
        //Targets.ToList().ForEach(x => x.OnDestroy += UpdateTargetCount);
    }

    private void UpdateHitCount() {
        hitCount -= 1;
        ControlGameState();
    }

    private void UpdateTargetCount() {
        targetCount -= 1;
        ControlGameState();
    }

    private void ControlGameState() {

        if (targetCount == 0) {
            var nextLevelIndex = level.index + 1;
            LevelManager.SharedInstance.UnlockLevel(nextLevelIndex);
            LevelManager.SharedInstance.LoadGameScene(nextLevelIndex);
            return;
        }

        if (hitCount <= 0) {
            LevelManager.SharedInstance.LoadLastActiveLevel();
            return;
        }

        hitLabel.text = hitCount.ToString();

    }

    private void OnDisable() {
        Ball.OnBallCollided -= UpdateHitCount;
        //Targets.ToList().ForEach(x => x.OnDestroy -= UpdateTargetCount);
    }

}
