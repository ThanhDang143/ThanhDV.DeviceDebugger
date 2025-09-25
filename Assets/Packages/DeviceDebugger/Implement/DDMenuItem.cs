#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ThanhDV.DeviceDebugger
{
    public class DDMenuItem
    {
        [MenuItem("GameObject/Device Debugger")]
        static void AddToSceneInGameObject()
        {
            AddToScene();
        }

        [MenuItem("Tools/Device Debugger/Add to Scene")]
        static void AddToScene()
        {
            // Find the script asset by type to get its path reliably.
            string scriptPath = GetScriptPath(typeof(DDMenuItem));
            if (string.IsNullOrEmpty(scriptPath))
            {
                Debug.Log($"<color=red>[DeviceDebugger] Could not find the DDMenuItem script asset path!!!</color>");
                return;
            }

            string scriptDirectory = System.IO.Path.GetDirectoryName(scriptPath);
            string prefabPath = System.IO.Path.Combine(scriptDirectory, "DeviceDebugger.prefab");

            // Load the prefab from the determined path
            GameObject originalPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (originalPrefab == null)
            {
                Debug.Log($"<color=red>[DeviceDebugger] Prefab not found at path: {prefabPath}. Please make sure DeviceDebugger.prefab is in the same directory as DDMenuItem.cs!!!</color>");
                return;
            }

            GameObject objectSource = Object.Instantiate(originalPrefab) as GameObject;
            objectSource.name = "DeviceDebugger";

            Debug.Log($"<color=green>[DeviceDebugger] GameObject DeviceDebugger instantiated in current scene!!!</color>");
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