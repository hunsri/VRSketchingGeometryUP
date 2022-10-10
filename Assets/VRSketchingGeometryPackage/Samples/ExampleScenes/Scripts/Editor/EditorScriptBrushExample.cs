using UnityEditor;
using UnityEngine;

namespace VRpen.Scripts.Examples.Editor
{
    [CustomEditor(typeof(BrushExample))]
    public class EditorScriptBrushExample : UnityEditor.Editor
    {
        private static int _resolution = 3;
        private static float _scale = 1.5f;
        private static int _interpolationSteps = 10;
        private static Color _color = Color.white;

        private bool _displayWarningResolutionTooLow = false;
        private bool _displayWarningScaleTooLow = false;
        private bool _displayWarningInterpolationStepsTooLow = false;
        private bool _displayWarningNoAlpha = false;

        private bool _displayErrorEditorNotInPlayMode = false;
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Defaults has to be set to a DefaultReferences.\n" +
                                    "DefaultReferences are Scriptable Objects, containing references to frequently" +
                                    " used prefabs, materials etc.", MessageType.Info);
            DrawDefaultInspector();
            
            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("Use the fields below to interact with the scene at runtime",
                MessageType.Info);

            BrushExample brushScript = (BrushExample) target;

            _resolution = EditorGUILayout.IntField("Resolution:", _resolution);
            _scale = EditorGUILayout.FloatField("Scale:", _scale);
            _interpolationSteps = EditorGUILayout.IntField("Interpolation Steps:", _interpolationSteps);
            _color = EditorGUILayout.ColorField("Color:", _color);
            
            if (GUILayout.Button("Create!"))
            {
                //Only create new lines if the editor is in Play Mode
                if (EditorApplication.isPlaying)
                {
                    brushScript.CreateCustomLineWithColor(_resolution, _scale, _interpolationSteps, _color);
                }
            }
            
            //Events have to be checked during Layout phase, otherwise an error will be thrown
            if (Event.current.type == EventType.Layout)
            {
                _displayWarningResolutionTooLow = (_resolution < 3);
                _displayWarningScaleTooLow = (_scale < 0);
                _displayWarningInterpolationStepsTooLow = (_interpolationSteps < 2);
                _displayWarningNoAlpha = (_color.a < 1);
                _displayErrorEditorNotInPlayMode = (!EditorApplication.isPlaying);
            }
            
            CheckAndDisplayWarnings();
        }

        private void CheckAndDisplayWarnings()
        {
            if (_displayErrorEditorNotInPlayMode)
            {
                EditorGUILayout.HelpBox("The editor isn't in Play Mode! Can't create objects.",
                    MessageType.Error);
            }
            
            if (_displayWarningResolutionTooLow)
            {
                EditorGUILayout.HelpBox("A resolution below 3 will cause issues. Are you sure?",
                    MessageType.Warning);
            }

            if (_displayWarningScaleTooLow)
            {
                EditorGUILayout.HelpBox("A scale below 0 will cause issues. Are you sure?",
                    MessageType.Warning);
            }

            if (_displayWarningInterpolationStepsTooLow)
            {
                EditorGUILayout.HelpBox("Less than 2 interpolation steps will cause issues. Are you sure?",
                    MessageType.Warning);
            }

            if (_displayWarningNoAlpha)
            {
                EditorGUILayout.HelpBox("The alpha channel isn't supported in this example!",
                    MessageType.Warning);
            }
        }
    }
}
