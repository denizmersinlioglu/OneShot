using UnityEngine;
using TMPro;
using UniRx;
using System.Linq;
using System;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI HitCountText;

    readonly CompositeDisposable disposibles = new CompositeDisposable();
    public int HitCount = 10;
    private int TargetsToDestory;
    private int DestroyedTargetCount;

    private BaseProjectile Ball {
        get { return FindObjectOfType<BaseProjectile>(); }
    }

    private BaseTarget[] Targets {
        get { return FindObjectsOfType<BaseTarget>(); }
    }

    private void SetupGame() {
        Application.targetFrameRate = 60;
        DestroyedTargetCount = 0;
        TargetsToDestory = Targets.Length;
        HitCountText.text = HitCount.ToString();
    }

    void Start() {

        SetupGame();
        
        Observable.FromEvent(
                    h => Ball.OnBallCollided += h,
                    h => Ball.OnBallCollided -= h)
                .ObserveOnMainThread()
                .Delay(TimeSpan.FromMilliseconds(100))
                .Subscribe(_ => {
                    if (HitCount <= 0) { return; }
                    HitCount--;
                    HitCountText.text = HitCount.ToString();

                    print("Destored Target Count: " + DestroyedTargetCount );
                    print("Total Target Count: " + TargetsToDestory);

                    if (HitCount <= 0 && DestroyedTargetCount != TargetsToDestory) {
                        Debug.Log("Level Failerd");
                        GetComponent<SceneTransition>().ReloadScene();
                    } else if (HitCount >= 0 && DestroyedTargetCount == TargetsToDestory) {
                        Debug.Log("Level Completed");
                    }
                })
                .AddTo(disposibles);


        Targets.ToList().ForEach(target => {
            Observable.FromEvent<BaseTarget>(
                    h => target.OnCollisonEntered += h,
                    h => target.OnCollisonEntered -= h)
                .ObserveOnMainThread()
                .Subscribe(x => {
                    print("Target hit with id: " + x.GetInstanceID());
                })
                .AddTo(disposibles);
        });

        Targets.ToList().ForEach(target => {
            Observable.FromEvent<BaseTarget>(
                    h => target.OnDestroy += h,
                    h => target.OnDestroy -= h)
                .ObserveOnMainThread()
                .Subscribe(x => {
                    print("Target destroyed with id: " + x.GetInstanceID());
                    x.Destroy();
                    DestroyedTargetCount++;
                })
                .AddTo(disposibles);
        });
    }

    private void OnDestroy() {
        disposibles.Dispose();
    }
}
