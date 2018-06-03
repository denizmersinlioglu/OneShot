﻿using UnityEngine;

public class BallBaseController : MonoBehaviour {

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
