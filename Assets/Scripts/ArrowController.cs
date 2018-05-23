using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public static float angle;

	private BallController ballController;
	private Vector3 horizontal;

	// Use this for initialization
	void Start () {
		ballController = GameObject.FindObjectOfType<BallController>();
		horizontal = new Vector3(1,0,0);

	}

	private void TrackTouch()
	{
		Vector3 trackTouch = ballController.startingPoint - Input.mousePosition;
		angle = Vector3.Angle(horizontal, trackTouch);
		Vector3 cross = Vector3.Cross(horizontal,trackTouch);
		if(cross.z < 0){ 
		angle = -angle;
		}
		this.transform.rotation = Quaternion.Euler(0,0,angle);
	}

	// Update is called once per frame
	void Update () {
		if(ballController.isTracking){
			this.transform.position = ballController.transform.position;
			TrackTouch();
		}

		if(ballController.inPlayMode){
			Destroy(gameObject);
		}

	}
}
