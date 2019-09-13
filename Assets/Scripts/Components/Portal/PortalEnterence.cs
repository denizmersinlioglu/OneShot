using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;
using DG.Tweening;

public class PortalEnterence : MonoBehaviour {
    readonly CompositeDisposable disposables = new CompositeDisposable();

    public Action<GameObject> ProjectilePenetration;

    void Start() {
        this.OnTriggerEnter2DAsObservable()
            .Where(_ => ProjectilePenetration != null)
            .Subscribe(x => {
                ProjectilePenetration(x.gameObject);
            })
            .AddTo(disposables);
    }

    public void PrepareToDestroy(float time) {
        transform.DOScale(0f, time);
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
