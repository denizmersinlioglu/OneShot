using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour {

#region Decleration Area
	public static int TN;
	public static int countDown ;

	public bool IsAccelModeOn;
	public bool IsAccelModeOnY = false;
	public bool IsVanishing = false;
	public float LaunchSpeed = 1f;
	public int hitLimit  =5 ;
	public int numberOfTargets =1;
	public float GravityScale = 0;
	public bool isKinematicAtStart = true;

	[HideInInspector]
	public bool inPlayMode = false;

	public Text hitText;
	[HideInInspector]
	public GameObject arrowFab;
	[HideInInspector]
	public bool isMute;
	[HideInInspector]
	public bool isTracking = false;
	[HideInInspector]
	public Rigidbody2D  m_rb;
	[HideInInspector]
	public Vector3 startingPoint;
	[HideInInspector]
	public Vector3 velocityVector;


	private Vector3 dir_Ball;
	int previousHit = -1;
	private int checker;
	private MusicManager musicManager2;
	private AudioSource audioSource2;
	private MusicManager musicManager;
	private AudioSource musicSource;
	private AudioSource audiosource;
	private GameObject arrow;
	private LevelManager levelManager;
	private Vector3 endPoint;
	private int hitCount =	0;

	#endregion

	#region Mute System
	public void Mute(){
	isMute = ! isMute;
	if(isMute){
		PlayerPrefControl.SetMasterVolume(0);
	}
	if(! isMute){
		PlayerPrefControl.SetMasterVolume(1);
	}
	}

	#endregion

	#region Level Controller Functions
	void TryAgain(){
		levelManager.LoadAgain();
	}


	void InvokeNextLevel(){
		levelManager.LoadNextLevel();
	}

	#endregion


	#region OnCollisionEnter2D 
	void OnCollisionEnter2D(Collision2D col)
	{
		if( ! isMute){
		audiosource.Play();
		}
		GameObject collider = col.gameObject;

		if(  ! collider.GetComponent<portal_Controller>())
		{
		hitCount = hitCount+1;
		}
	}

	#endregion

	#region Manuel Launching System
	public void LaunchStart()
	{
		if( ! IsAccelModeOn)
		{
		if(! inPlayMode)
		{
			if(IsVanishing)
		{
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
			foreach(GameObject target in targets)
			{
				target.GetComponent<SpriteRenderer>().enabled=false;
			}
		}
		isTracking = true;
		startingPoint = Input.mousePosition;
		arrow = Instantiate(arrowFab, this.transform.position,Quaternion.Euler(0f,0f,ArrowController.angle)) as GameObject;
		}
		}
	}

	public void LaunchEnd()
	{	
	if(! IsAccelModeOn){ 
		if(! inPlayMode)
		{
		if(IsVanishing)
		{
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
			foreach(GameObject target in targets)
			{
				target.GetComponent<SpriteRenderer>().enabled=true;
			}
		}
		m_rb.isKinematic = false;
		endPoint = Input.mousePosition;
		Vector3 directionVector = endPoint - startingPoint;
		dir_Ball = directionVector.normalized;
		velocityVector = tranformToVelocityVector(directionVector);
		m_rb.velocity =  -velocityVector;

		isTracking = false;
		inPlayMode = true;
		}
		}
	}

	private Vector3 tranformToVelocityVector (Vector3 vector)
	{
	Vector3 result = vector.normalized;
	result = result * LaunchSpeed;
	return result;
	}

	#endregion

	#region Acceleration Control System
	private void ControlWithAccel()
	{
			float x_Key = Input.GetAxis("Horizontal");
			float y_Key = Input.GetAxis("Vertical");

		float y =  Input.acceleration.y;
		float x = Input.acceleration.x;
		float z = Input.acceleration.z;
		m_rb.AddForce(new Vector2(LaunchSpeed*x_Key * Time.fixedDeltaTime, 0f));
		m_rb.AddForce(new Vector2(LaunchSpeed*x * Time.fixedDeltaTime, 0f));
	if(IsAccelModeOnY){
		m_rb.AddForce(new Vector2(0f, LaunchSpeed*y_Key*Time.fixedDeltaTime));
		m_rb.AddForce(new Vector2(0f, LaunchSpeed*y*Time.fixedDeltaTime));
	}
	}
	#endregion


	void Start () {
		//musicManager = GameObject.FindObjectOfType<MusicManager>();
		//musicSource = musicManager.GetComponent<AudioSource>();
		checker = 0;
		int volume = PlayerPrefControl.GetMasterVolume();
		if(volume == 0)
		{
			isMute=true;
		}
		if(volume== 1)
		{
			isMute=false;
		}
		audiosource = GetComponent<AudioSource>();
		countDown = hitLimit  ;
		hitCount = 0;
		inPlayMode = false;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		m_rb = this.GetComponent <Rigidbody2D>();
		m_rb.gravityScale = GravityScale;
		if( ! IsAccelModeOn)
		{
		m_rb.isKinematic = isKinematicAtStart;
		}
		if(IsAccelModeOn)
		{
		LaunchSpeed = LaunchSpeed * 50;
		}
		TN = numberOfTargets;

	}

	void FixedUpdate () {

	// if(! isMute){
	// musicSource.volume =1;
	// 	}else {
	// musicSource.volume =0;}


		if(IsAccelModeOn)
		{
			ControlWithAccel();
		}


		if(previousHit != hitCount && countDown != 0)
		{
		 previousHit = hitCount;
		 countDown = hitLimit - hitCount;
		 hitText.text = countDown.ToString();
		 }
		if(countDown == 0 && TN != 0)
		{
		Invoke("TryAgain",1f);
		hitText.text = "Fail";

		}
		if(TN == 0){
		hitText.text = "Win";
	
		if(checker == 0)
		{
				int loadedLevel = SceneManager.GetActiveScene().buildIndex;
				int nextLevel = loadedLevel +1;
				string nextLevelName = "Level" + nextLevel.ToString();
				PlayerPrefs.SetInt(nextLevelName,1);
				//LevelSceneController.savePlayerPrefences();
				checker =1;
		}
	
		Invoke("InvokeNextLevel",0.5f);
		}
	}
	}


