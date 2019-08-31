using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BaseTarget : MonoBehaviour {

    public Action OnDestroy;
    public Action OnCollisonEntered;

    public int RemainingHitToDestroy = 1;

    private void Start() {
        this.OnCollisionEnter2DAsObservable()
            .Where(x => x.gameObject.GetComponent<BaseBall>())
            .Subscribe(_ => OnCollisonEntered())
            .AddTo(this);

        this.OnCollisionEnter2DAsObservable()
            .Where(x => x.gameObject.GetComponent<BaseBall>())
            .Where(_ => RemainingHitToDestroy == 1)
            .Subscribe(_ => OnDestroy())
            .AddTo(this);

    }
}

