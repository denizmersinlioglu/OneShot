using UnityEngine;
using UniRx;

namespace LevelSystem {

    [System.Serializable]
    public class Level: MonoBehaviour {
        [HideInInspector]
        public BaseProjectile Projectile;
        [HideInInspector]
        public BaseTarget[] Targets;

        [HideInInspector]
        public CompositeDisposable disposables = new CompositeDisposable();

        public int index;

        public GameObject LevelStructure;
        public int MaxHitCount; 
        
        public float GravityScale; 
        public float ProjectileFriction;                                        
        public float ProjectileBounce;

        private void Awake() {
            Projectile = LevelStructure.gameObject.GetComponentInChildren<BaseProjectile>();
            Targets = LevelStructure.GetComponentsInChildren<BaseTarget>();
            Projectile.GetComponent<Rigidbody2D>().gravityScale = GravityScale;
        }

        private void OnDestroy() {
            disposables.Dispose();
        }

    }
}