#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateLevelList
{
    [MenuItem("Assets/Create/LevelList")]
    public static LevelList Create()
    {
        LevelList asset = ScriptableObject.CreateInstance<LevelList>();

        AssetDatabase.CreateAsset(asset, "Assets/LevelList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}

#endif