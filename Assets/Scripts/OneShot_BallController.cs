using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShot_BallController : MonoBehaviour {

	public float velocityConstant = 1f;
	private float velocityHolder = 1f;
	public Vector2 velo;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody2D>().velocity = velo;
	}
	
	// Update is called once per frame
	void Update () {
		if (velocityHolder != velocityConstant && velocityConstant != 0) {
			gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * velocityConstant ;
		}
		velocityHolder = velocityConstant;
		
	}
}
