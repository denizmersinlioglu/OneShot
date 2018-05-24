using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteButtonController : MonoBehaviour {

	public Sprite[] spriteArray;
	private BallController ballController;

	// Use this for initialization
	void Start () {

	ballController = GameObject.FindObjectOfType<BallController>();

	}
	
	// Update is called once per frame
	void Update () {

		if(ballController.isMute){
		Image image = this.GetComponent<Image>();
		image.sprite = spriteArray[0];
	}
		if( ! ballController.isMute){
		Image image = this.GetComponent<Image>();
		image.sprite = spriteArray[1];
	}
	}
}
