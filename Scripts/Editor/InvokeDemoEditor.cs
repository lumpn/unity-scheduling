//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn
{
    [CustomEditor(typeof(InvokeDemo))]
    public class InvokeDemoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.IntField("Invoke Counter", serializedObject.FindProperty("invokeCounter").intValue);
            EditorGUILayout.EndVertical();

            var demo = (InvokeDemo)target;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Invoke")) demo.DoInvoke();
            if (GUILayout.Button("Invoke Repeating")) demo.DoInvokeRepeating();
            if (GUILayout.Button("Cancel Invoke")) demo.DoCancelInvoke();
            EditorGUILayout.EndHorizontal();
        }
    }
}
