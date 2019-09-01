#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace LevelSystem
{
    public static class CreateLevelList
    {
        [MenuItem("Assets/Create/LevelList")]
        public static LevelList Create()
        {
            var asset = ScriptableObject.CreateInstance<LevelList>();

            AssetDatabase.CreateAsset(asset, "Assets/LevelList.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }
    }
}

#endif