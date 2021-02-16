#if UNITY_EDITOR
using HeyEscape.Core.Loaders;
using UnityEditor;
using UnityEngine;

namespace HeyEscape.EditorBehaviour
{
    [CustomEditor(typeof(LevelLoader))]
    public class LevelLoaderEditor : Editor
    {
        private void OnSceneGUI()
        {
            if (!Application.isPlaying) return;

            LevelLoader levelLoader = (LevelLoader)target;

            Handles.BeginGUI();
            if (GUILayout.Button("Kill all enemies", GUILayout.Width(100)))
            {
                levelLoader.KillAllEnemies();
            }
            Handles.EndGUI();
        }
    }
}
#endif