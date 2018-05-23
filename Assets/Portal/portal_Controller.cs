using UnityEngine;
using System.Collections;

public class portal_Controller : MonoBehaviour {

	public GameObject portalOpeningPosition;
	private BallController ball;
	private Animator animator;
	private Vector3 velocityKeeper;

	void OnTriggerEnter2D()
	{
		this.GetComponent<CircleCollider2D>().enabled = false;
		animator.enabled= true;
		ball.GetComponent<SpriteRenderer>().enabled=false;
		ball.GetComponent<CircleCollider2D>().enabled= false;
		velocityKeeper = ball.GetComponent<Rigidbody2D>().velocity;
		Invoke("DestroyPortal",0.65f);
		Invoke("CreatePortal",0.65f);
		Invoke("dest",1.25f);
	}

	void dest(){
		Destroy(gameObject);
	}

	void DestroyPortal(){
		this.GetComponent<SpriteRenderer>().enabled = false;
	}


	void CreatePortal()
	{	
		Destroy(portalOpeningPosition);
		ball.GetComponent<CircleCollider2D>().enabled= true;
		this.GetComponent<SpriteRenderer>().enabled = true;
		this.transform.position = portalOpeningPosition.transform.position;
		ball.transform.position= this.transform.position;
		ball.GetComponent<SpriteRenderer>().enabled = true;
		ball.GetComponent<Rigidbody2D>().velocity = velocityKeeper;

	}

	private int checker = 0;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		animator.enabled = false;
		ball = GameObject.FindObjectOfType<BallController>();
	}
	
	// Update is called once per frame
	void Update () {
		

	}
}
