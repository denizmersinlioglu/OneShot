using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
public class ObservableLaunchPointerTrigger : ObservableTriggerBase, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    Vector2? startPoint;
    Vector2? endPoint;

    public float IntervalSecond = 0.5f;

    Subject < (Vector2, Vector2, Vector2) > onLaunchPointerDown;
    Subject < (Vector2, Vector2, Vector2) > onMovePointerDown;

    float? raiseTime;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {
        raiseTime = Time.realtimeSinceStartup + IntervalSecond;
        startPoint = Input.mousePosition;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
        endPoint = Input.mousePosition;
        if (raiseTime <= Time.realtimeSinceStartup && startPoint != null && endPoint != null) {
            Vector2 diff = endPoint - startPoint ?? Vector2.zero;
            onLaunchPointerDown.OnNext((startPoint ?? Vector2.zero, endPoint ?? Vector2.zero, diff));
        }
    }
    void IDragHandler.OnDrag(PointerEventData eventData) {
        if (startPoint != null) {
            Vector2 currentPoint = Input.mousePosition;
            Vector2 diff = currentPoint - startPoint ?? Vector2.zero;
            onMovePointerDown.OnNext((startPoint ?? Vector2.zero, currentPoint, diff));
        }
    }
    public IObservable < (Vector2 start, Vector2 end, Vector2 diff) > OnLaunchPointerAsObservable() {
        var newSubject = new Subject < (Vector2, Vector2, Vector2) > ();
        return onLaunchPointerDown ?? (onLaunchPointerDown = newSubject);
    }

    public IObservable < (Vector2 start, Vector2 current, Vector2 diff) > OnMovePointerAsObservable() {
        var newSubject = new Subject < (Vector2, Vector2, Vector2) > ();
        return onMovePointerDown ?? (onMovePointerDown = newSubject);
    }
    protected override void RaiseOnCompletedOnDestroy() {
        if (onLaunchPointerDown != null) {
            onLaunchPointerDown.OnCompleted();
        }
    }

}