#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LevelSystem
{
    public class LevelEditor : EditorWindow
    {
        private readonly GUILayoutOption[] _options = { GUILayout.MaxWidth(300.0f), GUILayout.MinWidth(250.0f) };
        private readonly GUILayoutOption[] _topButtonOptions = { GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(125.0f) };

        public LevelList LevelList;
        private int _viewIndex = 1;

        [MenuItem("Window/Level Editor %#e")]
        private static void Init()
        {
            EditorWindow.GetWindow(typeof(LevelEditor));
        }

        private void OnEnable()
        {
            if (!EditorPrefs.HasKey("ObjectPath")) return;
            var objectPath = EditorPrefs.GetString("ObjectPath");
            LevelList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(LevelList)) as LevelList;
        }

        private void OnGUI()
        {

            if (LevelList != null)
            {
                GUILayout.Label("Level Editor", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Show Level Database", options: _topButtonOptions))
                {
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = LevelList;
                }
                if (GUILayout.Button("Open Level Database", options: _topButtonOptions))
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
                if (GUILayout.Button("Create New", _topButtonOptions))
                {
                    if (EditorUtility.DisplayDialog("Create Level List",
                        "Are you sure you want to create a new Level List ?", "Create", "Cancel"))
                    {
                        CreateNewLevelList();
                    }

                }
                if (GUILayout.Button("Open Existing", _topButtonOptions))
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
                        "Are you sure you want to delete Level" + _viewIndex + " ?", "Delete", "Cancel"))
                    {
                        DeleteItem(_viewIndex - 1);
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(5);

                GUILayout.Label("Navigation", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Prev Level", GUILayout.ExpandWidth(false)))
                {
                    if (_viewIndex > 1)
                        _viewIndex--;
                }
                GUILayout.Space(5);
                if (GUILayout.Button("Next Level", GUILayout.ExpandWidth(false)))
                {
                    if (_viewIndex < LevelList.LevelListDatabase.Count)
                    {
                        _viewIndex++;
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(15);

                if (LevelList.LevelListDatabase == null)
                    Debug.Log("wtf");
                if (LevelList.LevelListDatabase != null && LevelList.LevelListDatabase.Count > 0)
                {
                    EditorGUILayout.LabelField("Identification", EditorStyles.boldLabel);
                    GUILayout.BeginHorizontal();
                    _viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Index", _viewIndex, GUILayout.ExpandWidth(false)), 1, LevelList.LevelListDatabase.Count);
                    //Mathf.Clamp (viewIndex, 1, LevelList.levelList.Count);
                    EditorGUILayout.LabelField("of " + LevelList.LevelListDatabase.Count.ToString() + "  Levels", "", GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();
                    EditorGUI.BeginDisabledGroup(true);
                    LevelList.LevelListDatabase[_viewIndex - 1].Name = EditorGUILayout.TextField("Name", LevelList.LevelListDatabase[_viewIndex - 1].Name as string, _options);
                    EditorGUI.EndDisabledGroup();
                    LevelList.LevelListDatabase[_viewIndex - 1].Hint = EditorGUILayout.TextField("Hint", LevelList.LevelListDatabase[_viewIndex - 1].Hint as string, _options);

                    GUILayout.Space(15);

                    EditorGUILayout.LabelField("Structure", EditorStyles.boldLabel);
                    LevelList.LevelListDatabase[_viewIndex - 1].Structure = EditorGUILayout.ObjectField ("Construction Element", LevelList.LevelListDatabase[_viewIndex - 1].Structure, typeof (GameObject), false) as GameObject;
                
                    GUILayout.Space(15);

                    EditorGUILayout.LabelField("Progression Report", EditorStyles.boldLabel);

                    LevelList.LevelListDatabase[_viewIndex - 1].Status = (LevelStatus)EditorGUILayout.EnumPopup("Status:", LevelList.LevelListDatabase[_viewIndex - 1].Status, _options);
                    LevelList.LevelListDatabase[_viewIndex - 1].CompletionStatus = (LevelCompletionStatus)EditorGUILayout.EnumPopup("Completion Status:", LevelList.LevelListDatabase[_viewIndex - 1].CompletionStatus, _options);

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Control Types", EditorStyles.boldLabel);
                    LevelList.LevelListDatabase[_viewIndex - 1].IsAccelerometerActive = (bool)EditorGUILayout.Toggle("Accelerometer", LevelList.LevelListDatabase[_viewIndex - 1].IsAccelerometerActive, GUILayout.ExpandWidth(false));
                    LevelList.LevelListDatabase[_viewIndex - 1].IsLaunchActive = (bool)EditorGUILayout.Toggle("Launch", LevelList.LevelListDatabase[_viewIndex - 1].IsLaunchActive, GUILayout.ExpandWidth(false));
                    LevelList.LevelListDatabase[_viewIndex - 1].IsNavigationButtonsActive = (bool)EditorGUILayout.Toggle("Navigation Buttons", LevelList.LevelListDatabase[_viewIndex - 1].IsNavigationButtonsActive, GUILayout.ExpandWidth(false));

                    GUILayout.Space(15);

                    EditorGUILayout.LabelField("Hit Limits", EditorStyles.boldLabel);
                    LevelList.LevelListDatabase[_viewIndex - 1].ThreeStarHitLimit = EditorGUILayout.IntField("Three Stars Hit Limit #", LevelList.LevelListDatabase[_viewIndex - 1].ThreeStarHitLimit, GUILayout.ExpandWidth(false));
                    LevelList.LevelListDatabase[_viewIndex - 1].TwoStarHitLimit = EditorGUILayout.IntField("Two Stars Hit Limit #", LevelList.LevelListDatabase[_viewIndex - 1].TwoStarHitLimit, GUILayout.ExpandWidth(false));
                    LevelList.LevelListDatabase[_viewIndex - 1].MaximumHitCount = EditorGUILayout.IntField("Maximum Hit #", LevelList.LevelListDatabase[_viewIndex - 1].MaximumHitCount, GUILayout.ExpandWidth(false));

                    GUILayout.Space(15);

                    EditorGUILayout.LabelField("Physical Conditions", EditorStyles.boldLabel);
                    LevelList.LevelListDatabase[_viewIndex - 1].IsGravityEnabled = (bool)EditorGUILayout.Toggle("Gravitiy", LevelList.LevelListDatabase[_viewIndex - 1].IsGravityEnabled, GUILayout.ExpandWidth(false));
                    if (LevelList.LevelListDatabase[_viewIndex - 1].IsGravityEnabled)
                    {
                        LevelList.LevelListDatabase[_viewIndex - 1].GravityConstant = EditorGUILayout.FloatField("Gravitational Constant", LevelList.LevelListDatabase[_viewIndex - 1].GravityConstant, GUILayout.ExpandWidth(false));
                    }
                    else
                    {
                        EditorGUI.BeginDisabledGroup(!LevelList.LevelListDatabase[_viewIndex - 1].IsGravityEnabled);
                        LevelList.LevelListDatabase[_viewIndex - 1].GravityConstant = EditorGUILayout.FloatField("Gravitational Constant", 1.0f, GUILayout.ExpandWidth(false));
                        EditorGUI.EndDisabledGroup();
                    }

                    GUILayout.Space(5);

                    LevelList.LevelListDatabase[_viewIndex - 1].IsBallPhysicsEnabled = (bool)EditorGUILayout.Toggle("Ball Physics", LevelList.LevelListDatabase[_viewIndex - 1].IsBallPhysicsEnabled, GUILayout.ExpandWidth(false));
                    if (LevelList.LevelListDatabase[_viewIndex - 1].IsBallPhysicsEnabled)
                    {
                        LevelList.LevelListDatabase[_viewIndex - 1].BallFrictionRate = EditorGUILayout.FloatField("Ball Friction Rate", LevelList.LevelListDatabase[_viewIndex - 1].BallFrictionRate, GUILayout.ExpandWidth(false));
                    }
                    else
                    {
                        EditorGUI.BeginDisabledGroup(!LevelList.LevelListDatabase[_viewIndex - 1].IsBallPhysicsEnabled);
                        LevelList.LevelListDatabase[_viewIndex - 1].BallFrictionRate = EditorGUILayout.FloatField("Ball Friction Rate", 0.0f, GUILayout.ExpandWidth(false));
                        EditorGUI.EndDisabledGroup();
                    }

                    if (LevelList.LevelListDatabase[_viewIndex - 1].IsBallPhysicsEnabled)
                    {
                        LevelList.LevelListDatabase[_viewIndex - 1].BallBounceRate = EditorGUILayout.FloatField("Ball Bounce Rate", LevelList.LevelListDatabase[_viewIndex - 1].BallBounceRate, GUILayout.ExpandWidth(false));
                    }
                    else
                    {
                        EditorGUI.BeginDisabledGroup(!LevelList.LevelListDatabase[_viewIndex - 1].IsBallPhysicsEnabled);
                        LevelList.LevelListDatabase[_viewIndex - 1].BallBounceRate = EditorGUILayout.FloatField("Ball Bounce Rate", 1.0f, GUILayout.ExpandWidth(false));
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

        private void CreateNewLevelList()
        {
            // There is no overwrite protection here!
            // There is No "Are you sure you want to overwrite your existing object?" if it exists.
            // This should probably get a string from the user to create a new name and pass it ...
            _viewIndex = 1;
            LevelList = CreateLevelList.Create();
            if (!LevelList) return;
            LevelList.LevelListDatabase = new List<Level>();
            var relPath = AssetDatabase.GetAssetPath(LevelList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }

        private void OpenLevelList()
        {
            var absPath = EditorUtility.OpenFilePanel("Select Level List", "", "");
            if (!absPath.StartsWith(Application.dataPath)) return;
            var relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            LevelList = AssetDatabase.LoadAssetAtPath(relPath, typeof(LevelList)) as LevelList;
            if (LevelList != null && LevelList.LevelListDatabase == null)
                LevelList.LevelListDatabase = new List<Level>();
            if (LevelList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }

        private void AddItem()
        {
            var newLevel = new Level {Index = LevelList.LevelListDatabase.Count + 1};
            newLevel.Name = "Level" + newLevel.Index;
            LevelList.LevelListDatabase.Add(newLevel);
            _viewIndex = LevelList.LevelListDatabase.Count;
        }

        private void DeleteItem(int index)
        {
            LevelList.LevelListDatabase.RemoveAt(index);
        }
    }
}

#endif