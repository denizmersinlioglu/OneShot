using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
    public string scene = "<Insert scene name>";
    public float duration = 1.0f;
    public Color color = Color.black;
    public bool restartOnDoubleClick;

    private void Start() {
        if (!restartOnDoubleClick) { return; }
        var clickStream = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0));

        clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
            .Where(xs => xs.Count >= 2)
            .Subscribe(_ => {
                scene = SceneManager.GetActiveScene().name;
                PerformTransition();
                 //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            })
            .AddTo(this);
    }

    public void PerformTransition() {
        Transition.LoadLevel(scene, duration, color);
    }
}