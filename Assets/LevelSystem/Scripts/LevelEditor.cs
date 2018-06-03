using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : EditorWindow
{
    GUILayoutOption[] options = { GUILayout.MaxWidth(300.0f), GUILayout.MinWidth(250.0f) };
    GUILayoutOption[] topButtonOptions = { GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(125.0f) };

    public LevelList LevelList;
    private int viewIndex = 1;

    [MenuItem("Window/Level Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            LevelList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(LevelList)) as LevelList;
        }
    }

    void OnGUI()
    {

        if (LevelList != null)
        {
            GUILayout.Label("Level Editor", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Show Level List", options: topButtonOptions))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = LevelList;
            }
            if (GUILayout.Button("Open Level List", options: topButtonOptions))
            {
                OpenLevelList();
            }
            GUILayout.EndHorizontal();
        }


        if (LevelList == null)
        {
            GUILayout.Label("Welcome to the Level Editor", EditorStyles.boldLabel);

            GUILayout.Space(5);

            GUILayout.Label("Please create a new level editor or open an existing ", EditorStyles.label);
            GUILayout.Label("one to start", EditorStyles.label);

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create New", topButtonOptions))
            {
                if (EditorUtility.DisplayDialog("Create Level List",
                "Are you sure you want to create a new Level List ?", "Create", "Cancel"))
                {
                    CreateNewLevelList();
                }

            }
            if (GUILayout.Button("Open Existing", topButtonOptions))
            {
                OpenLevelList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(5);

        if (LevelList != null)
        {
            GUILayout.Label("Edit Level List", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add New Level", GUILayout.ExpandWidth(false)))
            {
                if (EditorUtility.DisplayDialog("Add New Level",
                "Are you sure you want to add a new level?", "Add", "Cancel"))
                {
                    AddItem();
                }
            }
            if (GUILayout.Button("Delete Level", GUILayout.ExpandWidth(false)))
            {
                EditorGUILayout.LabelField("This is an example of EditorWindow.ShowPopup", EditorStyles.wordWrappedLabel);
                GUILayout.Space(70);
                if (EditorUtility.DisplayDialog("Delete Level",
                "Are you sure you want to delete Level" + viewIndex + " ?", "Delete", "Cancel"))
                {
                    DeleteItem(viewIndex - 1);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUILayout.Label("Navigation", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Prev Level", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next Level", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < LevelList.levelList.Count)
                {
                    viewIndex++;
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(15);

            if (LevelList.levelList == null)
                Debug.Log("wtf");
            if (LevelList.levelList.Count > 0)
            {
                EditorGUILayout.LabelField("Identification", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Level Index", viewIndex, GUILayout.ExpandWidth(false)), 1, LevelList.levelList.Count);
                //Mathf.Clamp (viewIndex, 1, LevelList.levelList.Count);
                EditorGUILayout.LabelField("of " + LevelList.levelList.Count.ToString() + "  Levels", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();
                EditorGUI.BeginDisabledGroup(true);
                LevelList.levelList[viewIndex - 1].levelName = EditorGUILayout.TextField("Level Name", LevelList.levelList[viewIndex - 1].levelName as string, options);
                EditorGUI.EndDisabledGroup();
                LevelList.levelList[viewIndex - 1].levelHint = EditorGUILayout.TextField("Level Hint", LevelList.levelList[viewIndex - 1].levelHint as string, options);

                GUILayout.Space(15);

                EditorGUILayout.LabelField("Progression Report", EditorStyles.boldLabel);

                LevelList.levelList[viewIndex - 1].levelStatus = (LevelStatus)EditorGUILayout.EnumPopup("Level Status:", LevelList.levelList[viewIndex - 1].levelStatus, options);
                LevelList.levelList[viewIndex - 1].levelCompletionStatus = (LevelCompletionStatus)EditorGUILayout.EnumPopup("Level Completion Status:", LevelList.levelList[viewIndex - 1].levelCompletionStatus, options);

                GUILayout.Space(15);
                EditorGUILayout.LabelField("Control Types", EditorStyles.boldLabel);
                LevelList.levelList[viewIndex - 1].isAccelerometerActive = (bool)EditorGUILayout.Toggle("Accelerometer", LevelList.levelList[viewIndex - 1].isAccelerometerActive, GUILayout.ExpandWidth(false));
                LevelList.levelList[viewIndex - 1].isLaunchActive = (bool)EditorGUILayout.Toggle("Launch", LevelList.levelList[viewIndex - 1].isLaunchActive, GUILayout.ExpandWidth(false));
                LevelList.levelList[viewIndex - 1].isNavigationButtonsActive = (bool)EditorGUILayout.Toggle("NavigationButtons", LevelList.levelList[viewIndex - 1].isNavigationButtonsActive, GUILayout.ExpandWidth(false));

                GUILayout.Space(15);

                EditorGUILayout.LabelField("Hit Limits", EditorStyles.boldLabel);
                LevelList.levelList[viewIndex - 1].threeStarHitLimit = EditorGUILayout.IntField("Three Stars Hit Limit #", LevelList.levelList[viewIndex - 1].threeStarHitLimit, GUILayout.ExpandWidth(false));
                LevelList.levelList[viewIndex - 1].twoStarHitLimit = EditorGUILayout.IntField("Two Stars Hit Limit #", LevelList.levelList[viewIndex - 1].twoStarHitLimit, GUILayout.ExpandWidth(false));
                LevelList.levelList[viewIndex - 1].maximumHitCount = EditorGUILayout.IntField("Maximum Hit #", LevelList.levelList[viewIndex - 1].maximumHitCount, GUILayout.ExpandWidth(false));

                GUILayout.Space(15);

                EditorGUILayout.LabelField("Physical Conditions", EditorStyles.boldLabel);
                LevelList.levelList[viewIndex - 1].isGravityEnabled = (bool)EditorGUILayout.Toggle("Gravitiy", LevelList.levelList[viewIndex - 1].isGravityEnabled, GUILayout.ExpandWidth(false));
                if (LevelList.levelList[viewIndex - 1].isGravityEnabled)
                {
                    LevelList.levelList[viewIndex - 1].gravityConstant = EditorGUILayout.FloatField("Gravitational Constant", LevelList.levelList[viewIndex - 1].gravityConstant, GUILayout.ExpandWidth(false));
                }
                else
                {
                    EditorGUI.BeginDisabledGroup(!LevelList.levelList[viewIndex - 1].isGravityEnabled);
                    LevelList.levelList[viewIndex - 1].gravityConstant = EditorGUILayout.FloatField("Gravitational Constant", 1.0f, GUILayout.ExpandWidth(false));
                    EditorGUI.EndDisabledGroup();
                }

                GUILayout.Space(5);

                LevelList.levelList[viewIndex - 1].isBallPhysicsEnabled = (bool)EditorGUILayout.Toggle("Ball Physics", LevelList.levelList[viewIndex - 1].isBallPhysicsEnabled, GUILayout.ExpandWidth(false));
                if (LevelList.levelList[viewIndex - 1].isBallPhysicsEnabled)
                {
                    LevelList.levelList[viewIndex - 1].ballFrictionRate = EditorGUILayout.FloatField("Ball Friction Rate", LevelList.levelList[viewIndex - 1].ballFrictionRate, GUILayout.ExpandWidth(false));
                }
                else
                {
                    EditorGUI.BeginDisabledGroup(!LevelList.levelList[viewIndex - 1].isBallPhysicsEnabled);
                    LevelList.levelList[viewIndex - 1].ballFrictionRate = EditorGUILayout.FloatField("Ball Friction Rate", 0.0f, GUILayout.ExpandWidth(false));
                    EditorGUI.EndDisabledGroup();
                }

                if (LevelList.levelList[viewIndex - 1].isBallPhysicsEnabled)
                {
                    LevelList.levelList[viewIndex - 1].ballBounceRate = EditorGUILayout.FloatField("Ball Bounce Rate", LevelList.levelList[viewIndex - 1].ballBounceRate, GUILayout.ExpandWidth(false));
                }
                else
                {
                    EditorGUI.BeginDisabledGroup(!LevelList.levelList[viewIndex - 1].isBallPhysicsEnabled);
                    LevelList.levelList[viewIndex - 1].ballBounceRate = EditorGUILayout.FloatField("Ball Bounce Rate", 1.0f, GUILayout.ExpandWidth(false));
                    EditorGUI.EndDisabledGroup();
                }
            }
            else
            {
                GUILayout.Label("This Level List is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(LevelList);
        }
    }

    void CreateNewLevelList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        LevelList = CreateLevelList.Create();
        if (LevelList)
        {
            LevelList.levelList = new List<Level>();
            string relPath = AssetDatabase.GetAssetPath(LevelList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenLevelList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Level List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            LevelList = AssetDatabase.LoadAssetAtPath(relPath, typeof(LevelList)) as LevelList;
            if (LevelList.levelList == null)
                LevelList.levelList = new List<Level>();
            if (LevelList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        Level newLevel = new Level();
        newLevel.levelIndex = LevelList.levelList.Count + 1;
        newLevel.levelName = "Level" + newLevel.levelIndex;
        LevelList.levelList.Add(newLevel);
        viewIndex = LevelList.levelList.Count;
    }

    void DeleteItem(int index)
    {
        LevelList.levelList.RemoveAt(index);
    }
}