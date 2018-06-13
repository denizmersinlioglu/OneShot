#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LevelSystem
{
    public class LevelEditor : EditorWindow
    {
        private readonly GUILayoutOption[] options = { GUILayout.MaxWidth(300.0f), GUILayout.MinWidth(250.0f) };
        private readonly GUILayoutOption[] topButtonOptions = { GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(125.0f) };

        public LevelList levelList;
        private int viewIndex = 1;

        [MenuItem("Window/Level Editor %#e")]
        private static void Init()
        {
            EditorWindow.GetWindow(typeof(LevelEditor));
        }

        private void OnEnable()
        {
            if (!EditorPrefs.HasKey("ObjectPath")) return;
            var objectPath = EditorPrefs.GetString("ObjectPath");
            levelList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(LevelList)) as LevelList;
        }

        private void OnGUI()
        {

            if (levelList != null)
            {
                GUILayout.Label("Level Editor", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Show Level Database", options: topButtonOptions))
                {
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = levelList;
                }
                if (GUILayout.Button("Open Level Database", options: topButtonOptions))
                {
                    OpenLevelList();
                }
                GUILayout.EndHorizontal();
            }


            if (levelList == null)
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

            if (levelList != null)
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
                    if (viewIndex < levelList.levelListDatabase.Count)
                    {
                        viewIndex++;
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(15);

                if (levelList.levelListDatabase == null)
                    Debug.Log("wtf");
                if (levelList.levelListDatabase != null && levelList.levelListDatabase.Count > 0)
                {
                    EditorGUILayout.LabelField("Identification", EditorStyles.boldLabel);
                    GUILayout.BeginHorizontal();
                    viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Index", viewIndex, GUILayout.ExpandWidth(false)), 1, levelList.levelListDatabase.Count);
                    //Mathf.Clamp (viewIndex, 1, LevelList.levelList.Count);
                    EditorGUILayout.LabelField("of " + levelList.levelListDatabase.Count.ToString() + "  Levels", "", GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();
                    EditorGUILayout.LabelField("Name", levelList.levelListDatabase[viewIndex - 1].name as string, options);
                    levelList.levelListDatabase[viewIndex - 1].levelType = (LevelType)EditorGUILayout.EnumPopup("Type:", levelList.levelListDatabase[viewIndex - 1].levelType, options);
                    levelList.levelListDatabase[viewIndex - 1].hint = EditorGUILayout.TextField("Hint", levelList.levelListDatabase[viewIndex - 1].hint as string, options);

                    GUILayout.Space(15);

                    EditorGUILayout.LabelField("Structure", EditorStyles.boldLabel);
                    levelList.levelListDatabase[viewIndex - 1].structure = EditorGUILayout.ObjectField ("Construction Element", levelList.levelListDatabase[viewIndex - 1].structure, typeof (GameObject), false) as GameObject;
                
                    GUILayout.Space(15);

                    EditorGUILayout.LabelField("Progression Report", EditorStyles.boldLabel);

                    levelList.levelListDatabase[viewIndex - 1].status = (LevelStatus)EditorGUILayout.EnumPopup("Status:", levelList.levelListDatabase[viewIndex - 1].status, options);
                    levelList.levelListDatabase[viewIndex - 1].completionStatus = (LevelCompletionStatus)EditorGUILayout.EnumPopup("Completion Status:", levelList.levelListDatabase[viewIndex - 1].completionStatus, options);

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Control Types", EditorStyles.boldLabel);
                    levelList.levelListDatabase[viewIndex - 1].isAccelerometerActive = (bool)EditorGUILayout.Toggle("Accelerometer", levelList.levelListDatabase[viewIndex - 1].isAccelerometerActive, GUILayout.ExpandWidth(false));
                    levelList.levelListDatabase[viewIndex - 1].isLaunchActive = (bool)EditorGUILayout.Toggle("Launch", levelList.levelListDatabase[viewIndex - 1].isLaunchActive, GUILayout.ExpandWidth(false));
                    levelList.levelListDatabase[viewIndex - 1].isNavigationButtonsActive = (bool)EditorGUILayout.Toggle("Navigation Buttons", levelList.levelListDatabase[viewIndex - 1].isNavigationButtonsActive, GUILayout.ExpandWidth(false));

                    GUILayout.Space(15);
                    
                    EditorGUI.BeginDisabledGroup(levelList.levelListDatabase[viewIndex - 1].levelType == LevelType.hitBased);
                    EditorGUILayout.LabelField("Time Limits", EditorStyles.boldLabel);
                    levelList.levelListDatabase[viewIndex - 1].threeStarTimeLimit = EditorGUILayout.FloatField("Three Stars Time Limit", levelList.levelListDatabase[viewIndex - 1].threeStarTimeLimit, GUILayout.ExpandWidth(false));
                    levelList.levelListDatabase[viewIndex - 1].twoStarTimeLimit = EditorGUILayout.FloatField("Two Stars Time Limit", levelList.levelListDatabase[viewIndex - 1].twoStarTimeLimit, GUILayout.ExpandWidth(false));
                    levelList.levelListDatabase[viewIndex - 1].maximumTimeLimit = EditorGUILayout.FloatField("Maximum Time Limit", levelList.levelListDatabase[viewIndex - 1].maximumTimeLimit, GUILayout.ExpandWidth(false));
                    EditorGUI.EndDisabledGroup();
                    
                    GUILayout.Space(15);
                    
                    EditorGUI.BeginDisabledGroup(levelList.levelListDatabase[viewIndex - 1].levelType == LevelType.timeBased);
                    EditorGUILayout.LabelField("Hit Limits", EditorStyles.boldLabel);
                    levelList.levelListDatabase[viewIndex - 1].threeStarHitLimit = EditorGUILayout.IntField("Three Stars Hit Limit #", levelList.levelListDatabase[viewIndex - 1].threeStarHitLimit, GUILayout.ExpandWidth(false));
                    levelList.levelListDatabase[viewIndex - 1].twoStarHitLimit = EditorGUILayout.IntField("Two Stars Hit Limit #", levelList.levelListDatabase[viewIndex - 1].twoStarHitLimit, GUILayout.ExpandWidth(false));
                    levelList.levelListDatabase[viewIndex - 1].maximumHitCount = EditorGUILayout.IntField("Maximum Hit #", levelList.levelListDatabase[viewIndex - 1].maximumHitCount, GUILayout.ExpandWidth(false));
                    EditorGUI.EndDisabledGroup();
                    GUILayout.Space(15);

                    EditorGUILayout.LabelField("Physical Conditions", EditorStyles.boldLabel);
                    levelList.levelListDatabase[viewIndex - 1].isGravityEnabled = (bool)EditorGUILayout.Toggle("Gravitiy", levelList.levelListDatabase[viewIndex - 1].isGravityEnabled, GUILayout.ExpandWidth(false));
                    if (levelList.levelListDatabase[viewIndex - 1].isGravityEnabled)
                    {
                        levelList.levelListDatabase[viewIndex - 1].gravityConstant = EditorGUILayout.FloatField("Gravitational Constant", levelList.levelListDatabase[viewIndex - 1].gravityConstant, GUILayout.ExpandWidth(false));
                    }
                    else
                    {
                        EditorGUI.BeginDisabledGroup(!levelList.levelListDatabase[viewIndex - 1].isGravityEnabled);
                        levelList.levelListDatabase[viewIndex - 1].gravityConstant = EditorGUILayout.FloatField("Gravitational Constant", 1.0f, GUILayout.ExpandWidth(false));
                        EditorGUI.EndDisabledGroup();
                    }

                    GUILayout.Space(5);

                    levelList.levelListDatabase[viewIndex - 1].isBallPhysicsEnabled = (bool)EditorGUILayout.Toggle("Ball Physics", levelList.levelListDatabase[viewIndex - 1].isBallPhysicsEnabled, GUILayout.ExpandWidth(false));
                    if (levelList.levelListDatabase[viewIndex - 1].isBallPhysicsEnabled)
                    {
                        levelList.levelListDatabase[viewIndex - 1].ballFrictionRate = EditorGUILayout.FloatField("Ball Friction Rate", levelList.levelListDatabase[viewIndex - 1].ballFrictionRate, GUILayout.ExpandWidth(false));
                    }
                    else
                    {
                        EditorGUI.BeginDisabledGroup(!levelList.levelListDatabase[viewIndex - 1].isBallPhysicsEnabled);
                        levelList.levelListDatabase[viewIndex - 1].ballFrictionRate = EditorGUILayout.FloatField("Ball Friction Rate", 0.0f, GUILayout.ExpandWidth(false));
                        EditorGUI.EndDisabledGroup();
                    }

                    if (levelList.levelListDatabase[viewIndex - 1].isBallPhysicsEnabled)
                    {
                        levelList.levelListDatabase[viewIndex - 1].ballBounceRate = EditorGUILayout.FloatField("Ball Bounce Rate", levelList.levelListDatabase[viewIndex - 1].ballBounceRate, GUILayout.ExpandWidth(false));
                    }
                    else
                    {
                        EditorGUI.BeginDisabledGroup(!levelList.levelListDatabase[viewIndex - 1].isBallPhysicsEnabled);
                        levelList.levelListDatabase[viewIndex - 1].ballBounceRate = EditorGUILayout.FloatField("Ball Bounce Rate", 1.0f, GUILayout.ExpandWidth(false));
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
                EditorUtility.SetDirty(levelList);
            }
        }

        private void CreateNewLevelList()
        {
            // There is no overwrite protection here!
            // There is No "Are you sure you want to overwrite your existing object?" if it exists.
            // This should probably get a string from the user to create a new name and pass it ...
            viewIndex = 1;
            levelList = CreateLevelList.Create();
            if (!levelList) return;
            levelList.levelListDatabase = new List<Level>();
            var relPath = AssetDatabase.GetAssetPath(levelList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }

        private void OpenLevelList()
        {
            var absPath = EditorUtility.OpenFilePanel("Select Level List", "", "");
            if (!absPath.StartsWith(Application.dataPath)) return;
            var relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            levelList = AssetDatabase.LoadAssetAtPath(relPath, typeof(LevelList)) as LevelList;
            if (levelList != null && levelList.levelListDatabase == null)
                levelList.levelListDatabase = new List<Level>();
            if (levelList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }

        private void AddItem()
        {
            var newLevel = new Level {index = levelList.levelListDatabase.Count + 1};
            newLevel.name = "Level" + newLevel.index;
            levelList.levelListDatabase.Add(newLevel);
            viewIndex = levelList.levelListDatabase.Count;
        }

        private void DeleteItem(int index)
        {
            levelList.levelListDatabase.RemoveAt(index);
        }
    }
}

#endif