using UnityEngine;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;

public class BaseLauncher : MonoBehaviour {

    public GameObject TrajectoryPointPrefeb;

    public float TrajectoryStep = 0.02f;
    public int TrajectoryPointCount = 30;
    public float ThrowPower = 50;
    public float ThrowPowerMaxLimit = 15;
    public float ThrowPowerMinLimit = 5;
    public float TopMargin = 150;

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
            .Where(x => x.y < Screen.height - TopMargin)
            .RepeatUntilDestroy(this)
            .Subscribe(
                x => {
                    if (IsValidThrowPoint(x)) {
                        var force = GetForceFrom(Ball.transform.position, Camera.main.ScreenToWorldPoint(x));
                        Debug.Log(string.Format("{0} : {1}", force.magnitude, IsValidThrowPoint(x)));
                        float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
                        transform.eulerAngles = new Vector3(0, 0, angle);
                        SetupTrajectory(transform.position, force / Ball.GetComponent<Rigidbody2D>().mass);
                    } else {
                        trajectoryPoints.ForEach(p => p.GetComponent<SpriteRenderer>().enabled = false);
                    }
                })
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Select(_ => Input.mousePosition)
            .Subscribe(
                x => {
                    if (x.y < Screen.height - TopMargin && IsValidThrowPoint(x)) {
                        ThrowBall();
                        Destroy(gameObject);
                        trajectoryPoints.ForEach(p => Destroy(p.gameObject));
                    } else {
                        trajectoryPoints.ForEach(p => p.GetComponent<SpriteRenderer>().enabled = false);
                    }
                })
            .AddTo(this);



    }

    private void ThrowBall() {
        Ball.GetComponent<Rigidbody2D>().isKinematic = false;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var force = GetForceFrom(Ball.transform.position, mousePosition);
        Ball.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    private bool IsValidThrowPoint(Vector3 point) {
        var force = GetForceFrom(Ball.transform.position, Camera.main.ScreenToWorldPoint(point));
        return force.magnitude >= ThrowPowerMinLimit;
    }

    private Vector2 GetForceFrom(Vector2 fromPos, Vector2 toPos) {
        var distance = new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y);
        var force = distance * ThrowPower;
        if (force.magnitude > ThrowPowerMaxLimit) {
            var scaleFactor = ThrowPowerMaxLimit / force.magnitude;
            var scaleVector = new Vector2(scaleFactor, scaleFactor);
            force.Scale(scaleVector);
        }
        return -force;
    }

    void SetupTrajectory(Vector3 startPos, Vector2 vel) {
        var count = TrajectoryPointCount;
        var basePosition = (Vector2)Ball.transform.position;
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
