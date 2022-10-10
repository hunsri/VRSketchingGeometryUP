using UnityEngine;
using UnityEditor;

namespace VRpen.Scripts.Examples.Editor
{
    [CustomEditor(typeof(LineExample))]
    public class EditorScriptLineExample : UnityEditor.Editor
    {   
        private bool _displayErrorEditorNotInPlayMode = false;
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Defaults has to be set to a DefaultReferences.\n" +
                                    "DefaultReferences are Scriptable Objects, containing references to frequently" +
                                    " used prefabs, materials etc.", MessageType.Info);

            DrawDefaultInspector();
            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("Use the buttons below to interact with the scene at runtime", MessageType.Info);

            LineExample lineScript = (LineExample) target;
            if (GUILayout.Button("Undo"))
            {
                if (EditorApplication.isPlaying)
                {
                    lineScript.UndoActions(1);
                }
            }

            if (GUILayout.Button("Redo"))
            {
                if (EditorApplication.isPlaying)
                {
                    lineScript.RedoActions(1);    
                }
            }
            
            //Events have to be checked during Layout phase, otherwise an error will be thrown
            if (Event.current.type == EventType.Layout)
            {
                _displayErrorEditorNotInPlayMode = (!EditorApplication.isPlaying);
            }
            
            CheckAndDisplayWarnings();
        }
        
        private void CheckAndDisplayWarnings()
        {
            if (_displayErrorEditorNotInPlayMode)
            {
                EditorGUILayout.HelpBox("The editor isn't in Play Mode! Can't undo/redo actions.",
                    MessageType.Error);
            }
        }

    }
}
