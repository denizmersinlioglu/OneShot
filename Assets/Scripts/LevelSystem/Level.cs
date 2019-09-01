using UnityEngine;

namespace LevelSystem {

    [System.Serializable]
    public class Level: MonoBehaviour {
        [HideInInspector]
        public BaseProjectile Projectile;
        [HideInInspector]
        public BaseTarget[] Targets;

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
       
    }
}