using UnityEngine;

namespace LevelSystem {

    [System.Serializable]
    public class Level {

        public int index;

        public GameObject LevelStructure;
        public int MaxHitCount; 
        
        public float GravityConstant; 
        public float ProjectileFriction;                                        
        public float ProjectileBounce;
    }
}