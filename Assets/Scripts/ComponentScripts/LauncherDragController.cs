using UnityEngine;

public class LauncherDragController : MonoBehaviour
{

    [SerializeField]
    private float maxStretch = 2f;

    private GameObject ball;
    private Vector2 prevVelocity;
    private Rigidbody2D ballRigidBody;

    private LineRenderer lineRenderer;
    private SpringJoint2D spring;
    private Transform launcher;
    private Ray rayToMouse;
    private Ray launcherToProjectile;
    private float circleRadius;
    private bool clickedOn;


    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        launcher = spring.transform;
    }

    void Start()
    {

        ball = GameObject.FindGameObjectWithTag("OneShot_Ball");

        if (ball != null)
        {
            transform.position = ball.transform.position;
            ballRigidBody = ball.GetComponent<Rigidbody2D>();
            spring.connectedBody = ballRigidBody;
        }

        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, lineRenderer.transform.position);
        lineRenderer.sortingOrder = 10;
        rayToMouse = new Ray(launcher.position, Vector3.zero);
        launcherToProjectile = new Ray(lineRenderer.transform.position, Vector3.zero);


        CircleCollider2D circleCollider = ball.GetComponent<Collider2D>() as CircleCollider2D;
        circleRadius = circleCollider.radius;

    }

    void Update()
    {

        if (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("OneShot_Ball");
            transform.position = ball.transform.position;
            ballRigidBody = ball.GetComponent<Rigidbody2D>();
            spring.connectedBody = ballRigidBody;
        }

        if (clickedOn)
        {
            Dragging();
        }

        if (spring != null)
        {

            if (!ballRigidBody.isKinematic && prevVelocity.sqrMagnitude > ballRigidBody.velocity.sqrMagnitude)
            {
                Destroy(spring);
                ballRigidBody.velocity = prevVelocity;
            }

            if (!clickedOn)
            {
                prevVelocity = ballRigidBody.velocity;
            }

            LineRendererUpdate();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void OnMouseDown()
    {
        spring.enabled = false;
        clickedOn = true;

    }

    void OnMouseUp()
    {
        spring.enabled = true;
        ballRigidBody.isKinematic = false;
        clickedOn = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 ballToMouse = mouseWorldPoint - ball.transform.position;
        Vector3 launcherPosition = ball.transform.position - ballToMouse;

        var maxStretchSqr = maxStretch * maxStretch;
        if (ballToMouse.sqrMagnitude > maxStretchSqr)
        {
            rayToMouse.direction = -ballToMouse;
            launcherPosition = rayToMouse.GetPoint(maxStretch);
        }
        launcherPosition.z = 0;
        transform.position = launcherPosition;
    }

    void LineRendererUpdate()
    {
        Vector2 _launcherToProjectile = lineRenderer.transform.localPosition - ball.transform.position;
        launcherToProjectile.direction = _launcherToProjectile;
        Vector3 holdPoint = launcherToProjectile.GetPoint(_launcherToProjectile.magnitude - circleRadius);
        lineRenderer.SetPosition(1, holdPoint);
    }
}