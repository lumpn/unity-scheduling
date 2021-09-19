//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//----------------------------------------
using UnityEditor;

namespace Lumpn.Scheduling.Demo
{
    [CustomEditor(typeof(SchedulerHost))]
    public class SchedulerHostEditor : Editor<SchedulerHost>
    {
        public override void OnInspectorGUI(SchedulerHost host)
        {
            var scheduler = host.scheduler;
            EditorGUILayout.IntField("Queue length", scheduler.QueueLength);

            Repaint();
        }
    }
}
