using UnityEngine;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;

public class BaseLauncher: MonoBehaviour {

    public GameObject TrajectoryPointPrefeb;

    public float TrajectoryStep = 0.02f;
    public int TrajectoryPointCount = 30;
    public float ThrowPower = 50;

    private BaseProjectile Ball {
        get { return FindObjectOfType<BaseProjectile>(); }
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
                    SetupTrajectory(transform.position, vel / Ball.GetComponent<Rigidbody2D>().mass);
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

    void SetupTrajectory(Vector3 startPos, Vector2 vel) {
        var count = TrajectoryPointCount;
        var basePosition = (Vector2) Ball.transform.position;
        var velocity = Mathf.Sqrt((vel.x * vel.x) + (vel.y * vel.y));
        var angle = Mathf.Rad2Deg * (Mathf.Atan2(vel.y, vel.x));
        var fTime = 0f;

        fTime += TrajectoryStep;
        for (int i = 0; i < count; i++) {
            var gravityScale = Ball.GetComponent<Rigidbody2D>().gravityScale;
            var gravity = Physics2D.gravity.magnitude * gravityScale;

            var dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            var dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (gravity * fTime * fTime / 2.0f);

            trajectoryPoints[i].transform.position = basePosition + new Vector2(startPos.x + dx, startPos.y + dy);
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(vel.y - gravity * fTime, vel.x) * Mathf.Rad2Deg);

            trajectoryPoints[i].GetComponent<SpriteRenderer>().enabled = true;
            float alpha = (float)(count - i) / (float)count;
            trajectoryPoints[i].GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, alpha);

            fTime += TrajectoryStep;
        }
    }
}
