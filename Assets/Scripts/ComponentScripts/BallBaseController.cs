using System;
using UnityEngine;

namespace ComponentScripts
{
	public class BallBaseController : MonoBehaviour {

		public float VelocityConstant = 1f;
		private float _velocityHolder = 1f;
		public Vector2 Velo;
		// Use this for initialization
		private void Start () {
			gameObject.GetComponent<Rigidbody2D>().velocity = Velo;
		}
	
		// Update is called once per frame
		private void Update () {
			if (Math.Abs(_velocityHolder - VelocityConstant) > 10f && Math.Abs(VelocityConstant) < 1) {
				gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * VelocityConstant ;
			}
			_velocityHolder = VelocityConstant;
		
		}
	}
}
