using LevelSystem;
using UnityEngine;

namespace ComponentScripts {
	public class BallBaseController : MonoBehaviour {
		[SerializeField]
		private new ParticleSystem particleSystem;

		public delegate void BallCollide();
		public static event BallCollide BallCollideEvent;

		private float lastTime = 0f;
		private void OnCollisionEnter2D(Collision2D other) {
			if (BallCollideEvent != null) BallCollideEvent();
			Instantiate(particleSystem, other.contacts[0].point, Quaternion.identity);
		}

		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				if (Time.time - lastTime < 0.2f) {
					lastTime = Time.time;
					LevelManager.SharedInstance.LoadLastActiveLevel();
				} else {
					lastTime = Time.time;
					// turn on (or switch to) walking
				}
			}
		}
	}

}