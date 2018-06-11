using UnityEngine;

public class BorderController : MonoBehaviour
{
	[SerializeField]
	private GameObject topCollider;
	
	[SerializeField]
	private GameObject bottomCollider;
	
	[SerializeField]
	private GameObject leftCollider;
	
	[SerializeField]
	private GameObject rightCollider;

	private Vector2 resolution;
	
	private void Awake()
	{
		SetBorderColliders();
		resolution = new Vector2(Screen.width, Screen.height);
	}

	// Update is called once per frame
	private void Update () {
		if (resolution.x == Screen.width && resolution.y == Screen.height) return;
		
		SetBorderColliders();
		
		resolution.x = Screen.width;
		resolution.y = Screen.height;
	}

	private void SetBorderColliders()
	{
		var screenSize = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		topCollider.transform.position =new Vector3 (0,screenSize.y,0);
		topCollider.transform.localScale = new Vector3(screenSize.x,0.1f,0);
		
		bottomCollider.transform.position =new Vector3 (0,-screenSize.y,0);
		bottomCollider.transform.localScale = new Vector3(screenSize.x,0.1f,0);
		
		leftCollider.transform.position =new Vector3 (-screenSize.x,0,0);
		leftCollider.transform.localScale = new Vector3(0.1f,screenSize.y,0);

		rightCollider.transform.position =new Vector3 (screenSize.x,0,0);
		rightCollider.transform.localScale = new Vector3(0.1f,screenSize.y,0);
	}
}
