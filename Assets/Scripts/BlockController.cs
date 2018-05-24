using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

	public bool IsRotatingBlock;
	public float RotationSpeed;
	public bool IsMovingBlock;
	public float OscillationSpeed = 1f;
	public Transform Destination;
	public Transform RotatingBody;

	private float amplitudeX;
	private float amplitudeY;
	private Vector3 middlePoint;


	void Start () {
		middlePoint = (this.transform.position +Destination.position)/2;
		amplitudeX = middlePoint.x- transform.position.x;
		amplitudeY = middlePoint.y - transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (IsRotatingBlock)
		{
			RotatingBody.Rotate(0f,0f,Time.fixedDeltaTime * RotationSpeed*10f);
		}
		if(IsMovingBlock){
			float x = middlePoint.x + Mathf.Sin(Time.time * OscillationSpeed) * amplitudeX;
			float y = middlePoint.y + Mathf.Sin(Time.time * OscillationSpeed) * amplitudeY;
			this.transform.position = new Vector3( x,y,0f);
	}
	}
}
