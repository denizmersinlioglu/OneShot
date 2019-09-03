using UnityEngine;
using TMPro;
using UniRx;
using System.Linq;
using System;
using LevelSystem;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI HitCountText;

    public Level[] Levels;
    public IntReactiveProperty CurrentLevelIndex = new IntReactiveProperty(0);

    CompositeDisposable disposables = new CompositeDisposable();

    private readonly IntReactiveProperty HitCount = new IntReactiveProperty(0);
    private readonly IntReactiveProperty TargetsToDestory = new IntReactiveProperty(0);
    private readonly IntReactiveProperty DestroyedTargetCount = new IntReactiveProperty(0);


    public void LoadGame(Level levelPrefab) {
        var previousLevel = FindObjectOfType<Level>();
        if (previousLevel != null) Destroy(previousLevel.gameObject);
        SetupGame(Instantiate(levelPrefab, Vector3.zero, Quaternion.identity));
    }

    void Start() {
        Application.targetFrameRate = 60;
        CurrentLevelIndex.AsObservable()
            .ObserveOnMainThread()
            .Subscribe(x => LoadGame(Levels[x]))
            .AddTo(disposables);
    }

    private void OnDestroy() {
        disposables.Dispose();
    }

    private void LoadNextLevel() {
        Destroy(FindObjectOfType<Level>());
    }

    private void SetupGame(Level level) {
        GetComponent<BaseLauncher>().Setup(level);

        DestroyedTargetCount.Value = 0;
        TargetsToDestory.Value = level.Targets.Length;
        HitCount.Select(x => x.ToString()).SubscribeToText(HitCountText);
        HitCount.Value = level.MaxHitCount;
        SubscribeTo(level);
    }

    private void SubscribeTo(Level level) {

        Observable.CombineLatest(HitCount, DestroyedTargetCount, TargetsToDestory)
            .Where(x => x[0] <= 0 && x[1] != x[2])
            .Take(1)
            .Subscribe(_ => {
                Debug.Log("Level Failed");
                GetComponent<SceneTransition>().ReloadScene();
            })
            .AddTo(disposables);

        Observable.CombineLatest(HitCount, DestroyedTargetCount, TargetsToDestory)
            .Where(x => x[0] >= 0 && x[1] == x[2])
            .Take(1)
            .Subscribe(_ => {
                Debug.Log("Level Completed");
                GetComponent<SceneTransition>().ReloadScene();
            })
            .AddTo(disposables);

        Observable.FromEvent(
                    h => level.Projectile.OnBallCollided += h,
                    h => level.Projectile.OnBallCollided -= h)
            .ObserveOnMainThread()
            .TakeUntil(HitCount.Where(x => x <= 0))
            .Delay(TimeSpan.FromMilliseconds(100))
            .Subscribe(_ => {
                HitCount.Value--;
            })
            .AddTo(disposables);


        level.Targets.ToList().ForEach(target => {
            target.OnCollisonEntered += x => print("Target hit with id: " + x.GetInstanceID());

            Observable.FromEvent<BaseTarget>(
                    h => target.OnCollisonEntered += h,
                    h => target.OnCollisonEntered -= h)
                .ObserveOnMainThread()
                .Subscribe(x => {
                    print("Target hit with id: " + x.GetInstanceID());
                })
                .AddTo(disposables);

            Observable.FromEvent<BaseTarget>(
                    h => target.OnDestroy += h,
                    h => target.OnDestroy -= h)
                .ObserveOnMainThread()
                .Subscribe(x => {
                    print("Target destroyed with id: " + x.GetInstanceID());
                    x.Destroy();
                    DestroyedTargetCount.Value++;
                })
                .AddTo(disposables);
        });
    }
}
