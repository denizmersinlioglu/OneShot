using LevelSystem;
using UnityEngine;

namespace ComponentScripts
{
    public class LauncherDragController : MonoBehaviour
    {

        [SerializeField]
        private float maxStretch;

        private GameObject ball;
        private Vector3 pointerDownPoint;
        private Vector2 prevVelocity;
        private Rigidbody2D ballRigidBody;

        private LineRenderer lineRenderer;
        private SpringJoint2D spring;
        private Transform launcher;
        private Ray rayToMouse;
        private Ray offsetVector;
        private float circleRadius;
        private bool isLaunched;

       
        
        private void Awake()
        {
            spring = GetComponent<SpringJoint2D>();
            launcher = spring.transform;
        }

        private void Start()
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
            offsetVector = new Ray(lineRenderer.transform.position, Vector3.zero);

            var circleCollider = ball.GetComponent<Collider2D>() as CircleCollider2D;
            if (circleCollider != null) circleRadius = circleCollider.radius;
        }

        private void FixedUpdate()
        {
            
           
            if (ball == null)
            {
                ball = GameObject.FindGameObjectWithTag("OneShot_Ball");
                transform.position = ball.transform.position;
                ballRigidBody = ball.GetComponent<Rigidbody2D>();
                spring.connectedBody = ballRigidBody;
            }

            if (spring != null)
            {
                if (!ballRigidBody.isKinematic && prevVelocity.sqrMagnitude > ballRigidBody.velocity.sqrMagnitude)
                {
                    Destroy(spring);
                    ballRigidBody.velocity = prevVelocity;
                }

                if(isLaunched)
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

        // Launcher Control Panel Pointer Down
        public void pointerDown()
        {
            if (spring == null) return;
            pointerDownPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointerDownPoint.z = 0;
            spring.enabled = false;
            isLaunched = false;


        }
    
        // Launcher Control Panel Pointer Down
        public void pointerUp()
        {
            if (spring == null) return;
            isLaunched = true;
            spring.enabled = true;
            ballRigidBody.isKinematic = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
        
        // Launcher Control Panel Drag
        public void drag()
        {
            var pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var deltaMousePosition = pointerPosition - pointerDownPoint;
            var launcherPosition = ball.transform.position - deltaMousePosition;

            var maxStretchSqr = maxStretch * maxStretch;
            if (deltaMousePosition.sqrMagnitude > maxStretchSqr)
            {
                rayToMouse.direction = -deltaMousePosition;
                launcherPosition = rayToMouse.GetPoint(maxStretch);
            }
            launcherPosition.z = 0;
            transform.position = launcherPosition;
        }

        void LineRendererUpdate()
        {
            var launcherToProjectile = lineRenderer.transform.localPosition - ball.transform.position;
            offsetVector.direction = launcherToProjectile;
            var holdPoint = offsetVector.GetPoint(launcherToProjectile.magnitude - circleRadius);
            lineRenderer.SetPosition(1, holdPoint);
        }
    }
}