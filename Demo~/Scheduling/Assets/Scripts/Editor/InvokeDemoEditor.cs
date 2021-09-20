//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn.Scheduling.Demo
{
    [CustomEditor(typeof(InvokeDemo))]
    public class InvokeDemoEditor : Editor<InvokeDemo>
    {
        public override void OnInspectorGUI(InvokeDemo demo)
        {
            EditorGUILayout.IntField("Invoke Counter", demo.InvokeCounter);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Invoke")) demo.DoInvoke();
            if (GUILayout.Button("Invoke Repeating")) demo.DoInvokeRepeating();
            if (GUILayout.Button("Cancel Invoke")) demo.DoCancelInvoke();
            EditorGUILayout.EndHorizontal();

            Repaint();
        }
    }
}
