using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class UserInteractionController : MonoBehaviour {

    ObservableLaunchPointerTrigger launchTrigger;
    private void Start() {
        // SubscribeClickPosition();
        // SubscribeDoubleClick();
        SubscribeLaunch();
        SubscribeMove();
    }

    private void SubscribeLaunch() {
        if (gameObject.GetComponent(typeof(ObservableLaunchPointerTrigger)) == null) {
            launchTrigger = gameObject.AddComponent<ObservableLaunchPointerTrigger>();
        }
        launchTrigger.OnLaunchPointerAsObservable()
            .Subscribe(vector =>
                Debug.Log(string.Format("Launch -> Start {0}, End {1}, Diff {2}",
                    vector.start,
                    vector.end,
                    vector.diff))
            );
    }

    private void SubscribeMove() {
        if (gameObject.GetComponent(typeof(ObservableLaunchPointerTrigger)) == null) {
            launchTrigger = gameObject.AddComponent<ObservableLaunchPointerTrigger>();
        }
        launchTrigger.OnMovePointerAsObservable()
            .Where(vector => vector.diff.magnitude > 100)
            .Subscribe(vector =>
                Debug.Log(string.Format("Move -> Start {0}, Current {1}, Diff {2}",
                    vector.start,
                    vector.current,
                    vector.diff))
            );
    }

    private void SubscribeClickPosition() {
        var clickStream = this.OnMouseDownAsObservable()
            .Select(_ => Input.mousePosition);

        clickStream.Where(pos => pos.x >= 0)
            .Select(pos => pos.x)
            .Subscribe(x => Debug.Log("Clicked at X position: " + x.ToString()));
    }

    private void SubscribeDoubleClick() {
        var clickStream = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0));

        clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
            .Where(xs => xs.Count >= 2)
            .Subscribe(xs => Debug.Log("DoubleClick Detected! Count:" + xs.Count));
    }

}