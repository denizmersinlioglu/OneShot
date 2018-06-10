using System;
using UnityEngine;

namespace ComponentScripts
{
	public class BallBaseController : MonoBehaviour {

		public float velocityConstant = 1f;
		private float velocityHolder = 1f;
		public Vector2 velo;
		// Use this for initialization
		private void Start () {
			gameObject.GetComponent<Rigidbody2D>().velocity = velo;
		}
	
		// Update is called once per frame
		private void Update () {
			if (Math.Abs(velocityHolder - velocityConstant) > 10f && Math.Abs(velocityConstant) < 1) {
				gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * velocityConstant ;
			}
			velocityHolder = velocityConstant;
		
		}
	}
}
