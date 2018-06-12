using UnityEngine;

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

		// Use this for initialization
		private void Start () {
		
		}
	
		// Update is called once per frame
		private void Update () {
		
		}
	}
}
