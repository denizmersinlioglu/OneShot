using UnityEngine;
using LevelSystem;
using ComponentScripts;
using TMPro;

namespace SceneControllers
{
	public class GameController : MonoBehaviour {
	
		/*  
			Level Builder has already created the game environment according to current level.
	 		The aim of this class is control the game state according to existing level structure, control types and active levels win/lose statements.
	
			You may try to use update function to control state of the game.
			However game is based on hit counts. Therefore you need to use colliderEnter2D callbacks to control the game state.
			Need to create a notification - observer structure to get these callbacks from active levels ball.

	 		Just access to Level Builders active level to get these data from Level Database.
	 		Get hit count from level instance - Control win/lose state of the level.
     		Get success limit numbers from level instance - Control user success rate according to twoStarsHit# and threeStarsHit#.
    		When user complete the level - Update LevelDatabase asset according to users success.
 		*/

		[SerializeField]
		private LevelList levelDatabase;

		[SerializeField]
		private int levelNumber = 1;

		[SerializeField]
		private TextMeshProUGUI hitLabel;

		[SerializeField]
		private Level level;

		
		private int hitCount = 1000;
		private int targetCount = 1000;
		
		private void Awake()
		{
			level = LevelManager.SharedInstance != null ? LevelManager.SharedInstance.GetActiveLevel() : levelDatabase.levelListDatabase[levelNumber-1];
			targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
			hitCount = level.maximumHitCount;
			
			TargetBaseController.TargetDestroyedEvent += UpdateTargetCount;
			BallBaseController.BallCollideEvent += UpdateHitCount;
			
		}

		private void UpdateHitCount()
		{
			hitCount -= 1;
			ControlGameState();
		}

		private void UpdateTargetCount()
		{
			targetCount -= 1;
			ControlGameState();
		}

		private void ControlGameState()
		{

			if (targetCount == 0)
			{
				var nextLevelIndex = level.index + 1;
				LevelManager.SharedInstance.UnlockLevel(nextLevelIndex);
				LevelManager.SharedInstance.LoadGameScene(nextLevelIndex);
				return;
			}

			if (hitCount <= 0)
			{
				LevelManager.SharedInstance.LoadLastActiveLevel();
				return;
			}
			
			hitLabel.text = hitCount.ToString();

			
		}

		private void OnDisable()
		{
			BallBaseController.BallCollideEvent -= UpdateHitCount;
			TargetBaseController.TargetDestroyedEvent -= UpdateTargetCount;
		}

	}
}
