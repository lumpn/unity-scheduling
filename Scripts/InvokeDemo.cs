#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Lumpn
{
    public class InvokeDemo : MonoBehaviour
    {
        [SerializeField] private SchedulerHost host;
        [SerializeField] private int invokeCounter;

        [System.NonSerialized] private CancellationToken invokeToken = new CancellationToken();
        [System.NonSerialized] private CancellationToken invokeRepeatingToken = new CancellationToken();

        [ContextMenu("Invoke")]
        void InvokeSomething()
        {
            invokeToken = new CancellationToken();
            host.scheduler.Invoke(ScheduledInvoke, 5f, this, null, invokeToken);
        }

        [ContextMenu("Invoke Repeating")]
        void InvokeRepeating()
        {
            host.scheduler.InvokeRepeating(ScheduledInvoke, 1f, 5f, this, null, invokeRepeatingToken);
        }

        [ContextMenu("Cancel Invoke")]
        void CancelInvokes()
        {
            invokeToken.Cancel();
            invokeRepeatingToken.Cancel();
        }

        private static void ScheduledInvoke(object owner, object state)
        {
            var demo = (InvokeDemo)owner;
            demo.ScheduledInvoke();
        }

        private void ScheduledInvoke()
        {
            Profiler.BeginSample("ScheduledInvoke");
            invokeCounter++;
            Profiler.EndSample();
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            GUILayout.Label("Invoke counter");
            GUILayout.TextField(invokeCounter.ToString());
            GUILayout.EndHorizontal();
        }
    }
}
