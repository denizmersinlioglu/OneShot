using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;

using DG.Tweening;

public class Portal : MonoBehaviour {

    public PortalEnterence firstEnterence;
    public PortalEnterence secondEnterence;
    public IntReactiveProperty AllowedEnterenceCount = new IntReactiveProperty();
    Vector2 enterenceVelocity;
    Vector3 enterenceSize;

    public float PortalEnteranceDuration = 0.1f;
    public float PortalExitDuration = 0.1f;
    public float TransportationDelay = 0.4f;
    public float PortalDestroyDuration = 0.2f;

    float MinimumDestoryTime {
        get {
            return PortalEnteranceDuration + PortalExitDuration + TransportationDelay;
        }
    }

    readonly CompositeDisposable disposables = new CompositeDisposable();

    void Start() {
        SubscribeToEnterence(firstEnterence);
        SubscribeToEnterence(secondEnterence);
    }

    PortalEnterence OtherEnterence(PortalEnterence enterence) {
        return enterence == firstEnterence ? secondEnterence : firstEnterence;
    }

    void SubscribeToEnterence(PortalEnterence enterence) {
        var otherEnterence = OtherEnterence(enterence);
        Observable.FromEvent<GameObject>(
                     h => enterence.ProjectilePenetration += h,
                     h => enterence.ProjectilePenetration -= h)
             .ObserveOnMainThread()
             .Do(x => {
                 otherEnterence.GetComponent<CircleCollider2D>().enabled = false;
                 var rigidBody = x.GetComponent<Rigidbody2D>();
                 this.enterenceSize = x.transform.localScale;
                 this.enterenceVelocity = rigidBody.velocity;

                 rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                 x.transform.DOScale(0.01f, PortalEnteranceDuration);
                 x.transform.DOMove(enterence.transform.position, PortalEnteranceDuration);
             })
             .Delay(TimeSpan.FromSeconds(PortalEnteranceDuration))
             .Do(x => {
                 var spriteRenderer = x.GetComponent<SpriteRenderer>();
                 spriteRenderer.enabled = false;
                 
             })
             .Delay(TimeSpan.FromSeconds(TransportationDelay))
             .Subscribe(x => {
                 var rigidBody = x.GetComponent<Rigidbody2D>();
                 var spriteRenderer = x.GetComponent<SpriteRenderer>();
                 AllowedEnterenceCount.Value--;

                 x.transform.position = otherEnterence.transform.position;
                 x.transform.DOScale(enterenceSize, PortalExitDuration);

                 spriteRenderer.enabled = true;
                 rigidBody.constraints = RigidbodyConstraints2D.None;
                 rigidBody.velocity = enterenceVelocity;
             })
             .AddTo(disposables);

        AllowedEnterenceCount.AsObservable()
            .Where(x => x == 0)
            .Delay(TimeSpan.FromSeconds(MinimumDestoryTime))
            .Do(_ => {
                firstEnterence.PrepareToDestroy(PortalDestroyDuration);
                secondEnterence.PrepareToDestroy(PortalDestroyDuration);
            })
            .Delay(TimeSpan.FromSeconds(PortalDestroyDuration))
            .Subscribe(x => {
                Destroy(gameObject);
            })
            .AddTo(disposables);
    }
}
