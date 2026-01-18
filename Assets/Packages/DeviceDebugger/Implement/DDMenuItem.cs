#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ThanhDV.DeviceDebugger
{
    public class DDMenuItem
    {
        [MenuItem("GameObject/Device Debugger")]
        static void AddToSceneInGameObject()
        {
            AddToScene();
        }

        [MenuItem("Tools/ThanhDV/Device Debugger/Remove from Scene")]
        static void RemoveFromScene()
        {
            var ddControllers = Object.FindObjectsByType<DeviceDebuggerController>(FindObjectsSortMode.None);

            if (ddControllers.Length <= 0)
            {
                DebugLog.Warning("No DeviceDebugger instances found in the scene to remove.");
                return;
            }

            foreach (var controller in ddControllers)
            {
                Undo.DestroyObjectImmediate(controller.gameObject);
            }

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

            DebugLog.Success($"Removed {ddControllers.Length} DeviceDebugger instance(s) from the scene.");
        }

        [MenuItem("Tools/ThanhDV/Device Debugger/Add to Scene")]
        static void AddToScene()
        {
            if (Application.isPlaying)
            {
                DebugLog.Warning("Add to Scene is disabled in Play Mode. Stop Play Mode to add and save into the scene.");
                return;
            }

            // Find the script asset by type to get its path reliably.
            string scriptPath = GetScriptPath(typeof(DDMenuItem));
            if (string.IsNullOrEmpty(scriptPath))
            {
                DebugLog.Error("Could not find the DDMenuItem script asset path!!!");
                return;
            }

            string scriptDirectory = System.IO.Path.GetDirectoryName(scriptPath);
            // AssetDatabase paths must use forward slashes.
            scriptDirectory = (scriptDirectory ?? string.Empty).Replace('\\', '/');
            string prefabPath = $"{scriptDirectory}/DeviceDebugger.prefab";

            // Load the prefab from the determined path
            GameObject originalPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (originalPrefab == null)
            {
                DebugLog.Error($"Prefab not found at path: {prefabPath}. Please make sure DeviceDebugger.prefab is in the same directory as DDMenuItem.cs!!!");
                return;
            }

            var activeScene = SceneManager.GetActiveScene();

            GameObject objectSource = PrefabUtility.InstantiatePrefab(originalPrefab, activeScene) as GameObject;
            if (objectSource == null)
            {
                objectSource = Object.Instantiate(originalPrefab);
                SceneManager.MoveGameObjectToScene(objectSource, activeScene);
            }

            Undo.RegisterCreatedObjectUndo(objectSource, "Add DeviceDebugger");
            objectSource.name = "DeviceDebugger";

            // Ensure scene is marked dirty so Unity shows '*' and prompts to save on scene switch.
            EditorSceneManager.MarkSceneDirty(activeScene);
            Selection.activeGameObject = objectSource;

            DebugLog.Success("GameObject DeviceDebugger instantiated in current scene!!!");
        }

        /// <summary>
        /// Finds the path of a script file based on its class type, which is safer than searching by filename.
        /// </summary>
        /// <param name="type">The class type of the script to find.</param>
        /// <returns>The asset path of the script, or an empty string if not found.</returns>
        private static string GetScriptPath(System.Type type)
        {
            string[] guids = AssetDatabase.FindAssets($"t:script {type.Name}");
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var script = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
                if (script != null && script.GetClass() == type)
                {
                    return assetPath;
                }
            }
            return string.Empty;
        }
    }
}
#endif