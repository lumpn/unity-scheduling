//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//----------------------------------------
using UnityEngine;
using UnityEditor;

namespace Lumpn.Scheduling.Demo
{
    [CustomEditor(typeof(SchedulerHost))]
    public class SchedulerHostEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var host = (SchedulerHost)target;
            var scheduler = host.scheduler;

            if (scheduler != null)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.IntField("Queue length", scheduler.QueueLength);
                EditorGUILayout.EndVertical();

                Repaint();
            }
        }
    }
}
