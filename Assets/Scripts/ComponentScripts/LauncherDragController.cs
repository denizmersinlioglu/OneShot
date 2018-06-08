using UnityEngine;

namespace ComponentScripts
{
    public class LauncherDragController : MonoBehaviour
    {

        [SerializeField]
        private float _maxStretch = 2f;

        private GameObject _ball;
        private Vector2 _prevVelocity;
        private Rigidbody2D _ballRigidBody;

        private LineRenderer _lineRenderer;
        private SpringJoint2D _spring;
        private Transform _launcher;
        private Ray _rayToMouse;
        private Ray _launcherToProjectile;
        private float _circleRadius;
        private bool _clickedOn;


        void Awake()
        {
            _spring = GetComponent<SpringJoint2D>();
            _launcher = _spring.transform;
        }

        void Start()
        {

            _ball = GameObject.FindGameObjectWithTag("OneShot_Ball");

            if (_ball != null)
            {
                transform.position = _ball.transform.position;
                _ballRigidBody = _ball.GetComponent<Rigidbody2D>();
                _spring.connectedBody = _ballRigidBody;
            }

            _lineRenderer = GetComponent<LineRenderer>();

            _lineRenderer.SetPosition(0, _lineRenderer.transform.position);
            _lineRenderer.sortingOrder = 10;
            _rayToMouse = new Ray(_launcher.position, Vector3.zero);
            _launcherToProjectile = new Ray(_lineRenderer.transform.position, Vector3.zero);


            var circleCollider = _ball.GetComponent<Collider2D>() as CircleCollider2D;
            if (circleCollider != null) _circleRadius = circleCollider.radius;
        }

        void Update()
        {

            if (_ball == null)
            {
                _ball = GameObject.FindGameObjectWithTag("OneShot_Ball");
                transform.position = _ball.transform.position;
                _ballRigidBody = _ball.GetComponent<Rigidbody2D>();
                _spring.connectedBody = _ballRigidBody;
            }

            if (_clickedOn)
            {
                Dragging();
            }

            if (_spring != null)
            {

                if (!_ballRigidBody.isKinematic && _prevVelocity.sqrMagnitude > _ballRigidBody.velocity.sqrMagnitude)
                {
                    Destroy(_spring);
                    _ballRigidBody.velocity = _prevVelocity;
                }

                if (!_clickedOn)
                {
                    _prevVelocity = _ballRigidBody.velocity;
                }

                LineRendererUpdate();
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }

        void OnMouseDown()
        {
            _spring.enabled = false;
            _clickedOn = true;

        }

        void OnMouseUp()
        {
            _spring.enabled = true;
            _ballRigidBody.isKinematic = false;
            _clickedOn = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }

        void Dragging()
        {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 ballToMouse = mouseWorldPoint - _ball.transform.position;
            Vector3 launcherPosition = _ball.transform.position - ballToMouse;

            var maxStretchSqr = _maxStretch * _maxStretch;
            if (ballToMouse.sqrMagnitude > maxStretchSqr)
            {
                _rayToMouse.direction = -ballToMouse;
                launcherPosition = _rayToMouse.GetPoint(_maxStretch);
            }
            launcherPosition.z = 0;
            transform.position = launcherPosition;
        }

        void LineRendererUpdate()
        {
            Vector2 launcherToProjectile = _lineRenderer.transform.localPosition - _ball.transform.position;
            this._launcherToProjectile.direction = launcherToProjectile;
            Vector3 holdPoint = this._launcherToProjectile.GetPoint(launcherToProjectile.magnitude - _circleRadius);
            _lineRenderer.SetPosition(1, holdPoint);
        }
    }
}