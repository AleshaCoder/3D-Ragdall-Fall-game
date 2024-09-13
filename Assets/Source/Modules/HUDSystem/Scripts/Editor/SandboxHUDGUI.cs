using UnityEditor;
using UnityEngine;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SandboxHUD))]
    public class SandboxHUDGUI : Editor
    {
        private SandboxHUD _sandboxHUD;

        private void OnEnable()
        {
            _sandboxHUD = (SandboxHUD)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Set up scrolls of game objects"))
                _sandboxHUD.SetUpObjectsScrolls();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
