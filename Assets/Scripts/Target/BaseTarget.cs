using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BaseTarget : MonoBehaviour {

    public Action<BaseTarget> OnDestroy;
    public Action<BaseTarget> OnCollisonEntered;

    public int HitToDestroy = 1;

    private void Start() {
        this.OnCollisionEnter2DAsObservable()
            .Where(x => x.gameObject.GetComponent<BaseProjectile>())
            .Subscribe(_ => {
                HitToDestroy--;
                OnCollisonEntered?.Invoke(this);
                if (HitToDestroy == 0) {
                    OnDestroy?.Invoke(this);
                }
            })
            .AddTo(this);
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }
}

