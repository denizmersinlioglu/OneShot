using System;
using System.Collections;
using UnityEngine;

namespace ComponentScripts
{
	public class TargetBaseController : MonoBehaviour {

		private IEnumerator coroutine;
		private ParticleSystem ps;
		void OnCollisionEnter2D(Collision2D coll) {
			if (!coll.gameObject.CompareTag("OneShot_Ball")) return;
			ps = GetComponent<ParticleSystem>();
			var no = ps.noise;
			no.enabled = true;
			no.strength = 1.0f;
			no.quality = ParticleSystemNoiseQuality.High;

			var main = ps.main;
			main.loop = false;
		
			var collider = GetComponent<Collider2D>();
			if (collider == null) throw new ArgumentNullException("collider");
			collider.enabled = false;
			StartCoroutine(DestroyRectangle());
		}

		private IEnumerator DestroyRectangle()
		{
			yield return new WaitForSeconds(5);
			Destroy(gameObject);
		}
	}
}
