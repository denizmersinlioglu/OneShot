using UnityEngine;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;

public class CannonScript : MonoBehaviour {

    public GameObject TrajectoryPointPrefeb;

    public float TrajectoryStep = 0.04f;
    public int TrajectoryPointCount = 30;
    public float ThrowPower = 25;

    private BaseBall Ball {
        get { return FindObjectOfType<BaseBall>(); }
    }

    private List<GameObject> trajectoryPoints;

    void Start() {
        trajectoryPoints = new List<GameObject>();
        for (int i = 0; i < TrajectoryPointCount; i++) {
            GameObject dot = (GameObject)Instantiate(TrajectoryPointPrefeb);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Insert(i, dot);
        }

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .SelectMany(_ => gameObject.UpdateAsObservable())
            .TakeUntil(this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0)))
            .Select(_ => Input.mousePosition)
            .RepeatUntilDestroy(this)
            .Subscribe(
                x => {
                    Vector2 vel = GetForceFrom(Ball.transform.position, Camera.main.ScreenToWorldPoint(x));
                    float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle);
                    SetTrajectoryPoints(transform.position, vel / Ball.GetComponent<Rigidbody2D>().mass);
                })
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(
                _ => {
                    ThrowBall();
                    trajectoryPoints.ForEach(x => Destroy(x.gameObject));
                    Destroy(gameObject);
                }) 
            .AddTo(this);

    }

    private void ThrowBall() {
        Ball.GetComponent<Rigidbody2D>().isKinematic = false;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var force = GetForceFrom(Ball.transform.position, mousePosition);
        Ball.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    private Vector2 GetForceFrom(Vector2 fromPos, Vector2 toPos) {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * ThrowPower;
    }

    void SetTrajectoryPoints(Vector3 pStartPosition, Vector2 pVelocity) {
        var count = TrajectoryPointCount;
        var velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        var angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        var fTime = 0f;

        fTime += TrajectoryStep;
        for (int i = 0; i < count; i++) {
            var gravityScale = Ball.GetComponent<Rigidbody2D>().gravityScale;
            var gravity = Physics2D.gravity.magnitude * gravityScale;

            var dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            var dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (gravity * fTime * fTime / 2.0f);
            trajectoryPoints[i].transform.position = new Vector2(pStartPosition.x + dx, pStartPosition.y + dy);
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - gravity * fTime, pVelocity.x) * Mathf.Rad2Deg);

            trajectoryPoints[i].GetComponent<SpriteRenderer>().enabled = true;
            float alpha = (float)(count - i) / (float)count;
            trajectoryPoints[i].GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, alpha);

            fTime += TrajectoryStep;
        }
    }
}
