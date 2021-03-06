﻿using UnityEngine;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using LevelSystem;

public class BaseLauncher : MonoBehaviour {

    public GameObject TrajectoryPointPrefeb;

    public float TrajectoryStep = 0.01f;
    public int TrajectoryPointCount = 30;
    private float ThrowPower = 50;
    private float ThrowPowerMaxLimit = 15;
    private float ThrowPowerMinLimit = 5;
    private float TopMargin = 150;

    private float GravityScale;

    CompositeDisposable disposables;

    private List<GameObject> trajectoryPoints;

    private BaseProjectile Projectile;

    public void Setup(Level level) {
        disposables = new CompositeDisposable();

        trajectoryPoints = new List<GameObject>();
        for (int i = 0; i < TrajectoryPointCount; i++) {
            GameObject dot = (GameObject)Instantiate(TrajectoryPointPrefeb);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Insert(i, dot);
        }

        GravityScale = level.GravityScale;
        Projectile = level.Projectile;
        setupSubscriptions();
    }

    private void OnDisable() {
        disposables.Dispose();
    }
   
    private void ThrowBall() {
        Projectile.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var force = GetForceFrom(Projectile.transform.position, mousePosition);
        Projectile.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    private bool IsValidThrowPoint(Vector3 point) {
        var force = GetForceFrom(Projectile.transform.position, Camera.main.ScreenToWorldPoint(point));
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

    private void setupSubscriptions() {
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .SelectMany(_ => gameObject.UpdateAsObservable())
            .TakeUntil(this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0)))
            .Select(_ => Input.mousePosition)
            .Where(x => x.y < Screen.height - TopMargin)
            .RepeatUntilDestroy(this)
            .Subscribe(
                x => {
                    //Debug.Log(string.Format("{0} : {1}", force.magnitude, IsValidThrowPoint(x)));
                    if (IsValidThrowPoint(x)) {
                        var force = GetForceFrom(Projectile.transform.position, Camera.main.ScreenToWorldPoint(x));
                        float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
                        transform.eulerAngles = new Vector3(0, 0, angle);
                        SetupTrajectory(transform.position, force / Projectile.GetComponent<Rigidbody2D>().mass);
                    } else {
                        trajectoryPoints.ForEach(p => p.GetComponent<SpriteRenderer>().enabled = false);
                    }
                })
            .AddTo(disposables);

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Select(_ => Input.mousePosition)
            .Subscribe(
                x => {
                    if (x.y < Screen.height - TopMargin && IsValidThrowPoint(x)) {
                        ThrowBall();
                        this.enabled = false;
                        trajectoryPoints.ForEach(p => Destroy(p.gameObject));
                    } else {
                        trajectoryPoints.ForEach(p => p.GetComponent<SpriteRenderer>().enabled = false);
                    }
                })
            .AddTo(disposables);
    }

    void SetupTrajectory(Vector3 startPos, Vector2 vel) {
        var count = TrajectoryPointCount;
        var basePosition = (Vector2)Projectile.transform.position;
        var velocity = Mathf.Sqrt((vel.x * vel.x) + (vel.y * vel.y));
        var angle = Mathf.Rad2Deg * (Mathf.Atan2(vel.y, vel.x));
        var fTime = 0f;

        fTime += TrajectoryStep;
        for (int i = 0; i < count; i++) {
            var gravity = Physics2D.gravity.magnitude * GravityScale;

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
