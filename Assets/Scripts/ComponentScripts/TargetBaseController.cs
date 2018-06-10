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
			
			coroutine = FadeTo(0f, 0.5f);
			StartCoroutine(coroutine);

			var collider = GetComponent<Collider2D>();
			if (collider == null) throw new ArgumentNullException("collider");
			collider.enabled = false;
			StartCoroutine(DestroyRectangle());
		}

		private IEnumerator FadeTo(float aValue, float aTime)
		{
			var alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
			for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				var newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
				transform.GetComponent<SpriteRenderer>().material.color = newColor;
				yield return null;
			}
		}

		private IEnumerator DestroyRectangle()
		{
			yield return new WaitForSeconds(5);
			Destroy(gameObject);
		}
	}
}
