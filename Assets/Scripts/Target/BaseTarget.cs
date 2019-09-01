using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BaseTarget : MonoBehaviour {

    public Action OnDestroy;
    public Action OnCollisonEntered;

    public int HitToDestroy = 1;

    private void Start() {
        this.OnCollisionEnter2DAsObservable()
            .Where(x => x.gameObject.GetComponent<BaseProjectile>())
            .Subscribe(_ => OnCollisonEntered?.Invoke())
            .AddTo(this);

        this.OnCollisionEnter2DAsObservable()
            .Where(x => x.gameObject.GetComponent<BaseProjectile>())
            .Where(_ => HitToDestroy == 1)
            .Subscribe(_ => OnDestroy?.Invoke())
            .AddTo(this);

    }
}

