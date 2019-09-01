using UnityEngine;
using TMPro;
using UniRx;
using System.Linq;
using System;
using LevelSystem;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI HitCountText;

    public Level LevelPrefab;

    public int HitCount;
    private int TargetsToDestory;
    private int DestroyedTargetCount;

    void Start() {
        Application.targetFrameRate = 60;

        var instance = Instantiate(LevelPrefab, Vector3.zero, Quaternion.identity);
        SetupGame(instance);
        SubscribeTo(instance);
        SetupLauncher(instance);
    }

    private void SetupLauncher(Level level) {
        var launcher = GetComponent<BaseLauncher>();
        launcher.Setup(level);
    }

    private void SetupGame(Level level) {
        this.DestroyedTargetCount = 0;
        this.TargetsToDestory = level.Targets.Length;
        this.HitCountText.text = level.MaxHitCount.ToString();
        this.HitCount = level.MaxHitCount;
    }

    private void SubscribeTo(Level level) {

        Observable.FromEvent(
                    h => level.Projectile.OnBallCollided += h,
                    h => level.Projectile.OnBallCollided -= h)
                .ObserveOnMainThread()
                .Delay(TimeSpan.FromMilliseconds(100))
                .Subscribe(_ => {
                    if (HitCount <= 0) { return; }
                    HitCount--;
                    HitCountText.text = HitCount.ToString();

                    print("Destored Target Count: " + DestroyedTargetCount);
                    print("Total Target Count: " + TargetsToDestory);

                    if (HitCount <= 0 && DestroyedTargetCount != TargetsToDestory) {
                        Debug.Log("Level Failerd");
                        GetComponent<SceneTransition>().ReloadScene();
                    } else if (HitCount >= 0 && DestroyedTargetCount == TargetsToDestory) {
                        Debug.Log("Level Completed");
                    }
                })
                .AddTo(this);


        level.Targets.ToList().ForEach(target => {
            target.OnCollisonEntered += x => print("Target hit with id: " + x.GetInstanceID());

            Observable.FromEvent<BaseTarget>(
                    h => target.OnCollisonEntered += h,
                    h => target.OnCollisonEntered -= h)
                .ObserveOnMainThread()
                .Subscribe(x => {
                    print("Target hit with id: " + x.GetInstanceID());
                })
                .AddTo(this);
        });

        level.Targets.ToList().ForEach(target => {
            Observable.FromEvent<BaseTarget>(
                    h => target.OnDestroy += h,
                    h => target.OnDestroy -= h)
                .ObserveOnMainThread()
                .Subscribe(x => {
                    print("Target destroyed with id: " + x.GetInstanceID());
                    x.Destroy();
                    DestroyedTargetCount++;
                })
                .AddTo(this);
        });
    }
}
