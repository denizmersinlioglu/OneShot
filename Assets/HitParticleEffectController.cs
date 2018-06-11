using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleEffectController : MonoBehaviour {

	
	// Use this for initialization
	private void Start ()
	{
		StartCoroutine(Destroy());
	}

	private IEnumerator Destroy()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}
}
