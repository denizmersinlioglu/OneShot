
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LevelSystem {

    public class LevelDatabase : ScriptableObject {
        public List<Level> List;
    }

    public class LevelEditor : EditorWindow {
        private readonly GUILayoutOption[] options = { GUILayout.MaxWidth(300.0f), GUILayout.MinWidth(250.0f) };
        private readonly GUILayoutOption[] topButtonOptions = { GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(125.0f) };

        public LevelDatabase LevelDatabase;
        private int CurrentLevelIndex = 1;

        [MenuItem("Assets/Create/LevelList")]
        public static LevelDatabase CreateLevelDatabase() {
            var asset = ScriptableObject.CreateInstance<LevelDatabase>();
            AssetDatabase.CreateAsset(asset, "Assets/Levels/LevelList.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }

        [MenuItem("Window/Level Editor %#e")]
        private static void Init() {
            EditorWindow.GetWindow(typeof(LevelEditor));
        }

        private void OnEnable() {
            if (!EditorPrefs.HasKey("ObjectPath")) return;
            var objectPath = EditorPrefs.GetString("ObjectPath");
            LevelDatabase = AssetDatabase.LoadAssetAtPath(objectPath, typeof(LevelDatabase)) as LevelDatabase;
        }

        private void OnGUI() {

            if (LevelDatabase != null) {
                GUILayout.Label("Level Editor", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Show Level Database", options: topButtonOptions)) {
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = LevelDatabase;
                }
                if (GUILayout.Button("Open Level Database", options: topButtonOptions)) {
                    OpenLevelList();
                }
                GUILayout.EndHorizontal();
            }


            if (LevelDatabase == null) {
                GUILayout.Label("Welcome to the Level Editor", EditorStyles.boldLabel);

                GUILayout.Space(5);

                GUILayout.Label("Please create a new level editor or open an existing one to start", EditorStyles.label);

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Create New", topButtonOptions)) {
                    if (EditorUtility.DisplayDialog("Create Level List",
                        "Are you sure you want to create a new Level List ?", "Create", "Cancel")) {
                        CreateNewLevelList();
                    }

                }
                if (GUILayout.Button("Open Existing", topButtonOptions)) {
                    OpenLevelList();
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(5);

            if (LevelDatabase != null) {
                GUILayout.Label("Edit Level List", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add New Level", GUILayout.ExpandWidth(false))) {
                    if (EditorUtility.DisplayDialog("Add New Level",
                        "Are you sure you want to add a new level?", "Add", "Cancel")) {
                        AddItem();
                    }
                }
                if (GUILayout.Button("Delete Level", GUILayout.ExpandWidth(false))) {
                    EditorGUILayout.LabelField("This is an example of EditorWindow.ShowPopup", EditorStyles.wordWrappedLabel);
                    GUILayout.Space(70);
                    if (EditorUtility.DisplayDialog("Delete Level",
                        "Are you sure you want to delete Level" + CurrentLevelIndex + " ?", "Delete", "Cancel")) {
                        DeleteItem(CurrentLevelIndex - 1);
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(5);

                GUILayout.Label("Navigation", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Prev Level", GUILayout.ExpandWidth(false))) {
                    if (CurrentLevelIndex > 1)
                        CurrentLevelIndex--;
                }
                GUILayout.Space(5);
                if (GUILayout.Button("Next Level", GUILayout.ExpandWidth(false))) {
                    if (CurrentLevelIndex < LevelDatabase.List.Count) {
                        CurrentLevelIndex++;
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(15);

                if (LevelDatabase.List.Count == 0) {
                    GUILayout.Label("This Level List is Empty.");
                    return;
                }

                var level = LevelDatabase.List[CurrentLevelIndex - 1];
                var expandOption = GUILayout.ExpandWidth(false);
                EditorGUILayout.LabelField("Identification", EditorStyles.boldLabel);

                GUILayout.BeginHorizontal();

                var totalLevelCount = LevelDatabase.List.Count;
                CurrentLevelIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Index", CurrentLevelIndex, expandOption), 1, totalLevelCount);
                EditorGUILayout.LabelField("of " + LevelDatabase.List.Count.ToString() + "  Levels", "", expandOption);

                GUILayout.EndHorizontal();

                GUILayout.Space(15);

                level.LevelStructure = EditorGUILayout.ObjectField("Level Structure", level.LevelStructure, typeof(GameObject), false) as GameObject;

                GUILayout.Space(15);

                level.MaxHitCount = EditorGUILayout.IntField("Max Hit Count", level.MaxHitCount, expandOption);

                GUILayout.Space(15);

                level.GravityConstant = EditorGUILayout.FloatField("Gravitiy", level.GravityConstant, expandOption);

                GUILayout.Space(5);

                level.ProjectileFriction = EditorGUILayout.FloatField("Projetile Friction Rate", level.ProjectileFriction, expandOption);
                level.ProjectileBounce = EditorGUILayout.FloatField("Projetile Bounce Rate", level.ProjectileBounce, expandOption);
            }
            if (GUI.changed) {
                EditorUtility.SetDirty(LevelDatabase);
            }
        }

        private void CreateNewLevelList() {
            CurrentLevelIndex = 1;
            LevelDatabase = CreateLevelDatabase();
            LevelDatabase.List = new List<Level>();
            var relPath = AssetDatabase.GetAssetPath(LevelDatabase);
            EditorPrefs.SetString("ObjectPath", relPath);
        }

        private void OpenLevelList() {
            var absPath = EditorUtility.OpenFilePanel("Select Level List", "", "");
            if (!absPath.StartsWith(Application.dataPath, System.StringComparison.Ordinal)) return;
            var relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            LevelDatabase = AssetDatabase.LoadAssetAtPath(relPath, typeof(LevelDatabase)) as LevelDatabase;
            if (LevelDatabase != null && LevelDatabase.List == null)
                LevelDatabase.List = new List<Level>();
            if (LevelDatabase) {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }

        private void AddItem() {
            var newLevel = new Level { index = LevelDatabase.List.Count + 1 };
            LevelDatabase.List.Add(newLevel);
            CurrentLevelIndex = LevelDatabase.List.Count;
        }

        private void DeleteItem(int index) {
            LevelDatabase.List.RemoveAt(index);
        }
    }
}
