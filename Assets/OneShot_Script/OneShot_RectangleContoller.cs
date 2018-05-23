using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShot_RectangleContoller : MonoBehaviour {

	private IEnumerator coroutine;
	private ParticleSystem ps;
	void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "OneShot_Ball"){
			ps = GetComponent<ParticleSystem>();
        	var no = ps.noise;
        	no.enabled = true;
        	no.strength = 1.0f;
        	no.quality = ParticleSystemNoiseQuality.High;

			var main = ps.main;
        	main.loop = false;
			
			coroutine = FadeTo(0f, 0.5f);
        	StartCoroutine(coroutine);

			Collider2D collider = GetComponent<Collider2D>();
			collider.enabled = false;
			 StartCoroutine(DestroyRectangle());
		}  
    }

	IEnumerator FadeTo(float aValue, float aTime)
     {
         float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
             transform.GetComponent<SpriteRenderer>().material.color = newColor;
             yield return null;
         }
     }

	  IEnumerator DestroyRectangle()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
