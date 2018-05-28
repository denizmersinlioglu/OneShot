using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class LevelEditorWindow : EditorWindow {
	string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/LevelEditorWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LevelEditorWindow window = (LevelEditorWindow)EditorWindow.GetWindow(typeof(LevelEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Levels", EditorStyles.boldLabel);
        List<Level> myLevelList = new List<Level>();
        Level newLevel = new Level(5,LevelControlType.none, LevelStatus.none);
        myString = EditorGUILayout.TextField("Text Field", myString);
        newLevel.number = EditorGUILayout.IntField("Level number", newLevel.number);
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }
}
