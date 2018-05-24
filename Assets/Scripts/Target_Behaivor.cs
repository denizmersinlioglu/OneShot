using UnityEngine;
using System.Collections;

public class Target_Behaivor : MonoBehaviour {

	public bool IsMovingTarget;
	public Transform movingTarget;
	public float OscillationSpeed = 1f;
	public float DestroyTime = 0.05f;

	private LevelManager levelManager;

	void OnCollisionEnter2D()
	{
	 if(BallController.countDown != 0){
		Invoke("DestroyRect",DestroyTime);
		BallController.TN =  BallController.TN-1;
		}
	}



	void DestroyRect(){
		Destroy(gameObject);
	}

	private float amplitudeX;
	private float amplitudeY;
	private Vector3 middlePoint;
	// Use this for initialization
	void Start () {
		middlePoint = (this.transform.position + movingTarget.position)/2;
		amplitudeX = middlePoint.x- transform.position.x;
		amplitudeY = middlePoint.y - transform.position.y;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}


	void FixedUpdate()
	{
			if(IsMovingTarget){
			float x = middlePoint.x + Mathf.Sin(Time.time * OscillationSpeed) * amplitudeX;
			float y = middlePoint.y + Mathf.Sin(Time.time * OscillationSpeed) * amplitudeY;
			this.transform.position = new Vector3( x,y,0f);
	}
}
	}
