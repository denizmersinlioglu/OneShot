using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HintController : MonoBehaviour {

	public GameObject hintText;

	public void enableHint()
	{
		hintText.GetComponent<Text>().enabled = true;
		this.GetComponentInChildren<Text>().enabled= false;
	}

}
