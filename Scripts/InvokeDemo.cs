//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using UnityEngine;
using UnityEngine.Profiling;

#pragma warning disable CS0649

namespace Lumpn
{
    public class InvokeDemo : MonoBehaviour
    {
        [SerializeField] private SchedulerHost host;

        [System.NonSerialized] private CancellationToken token = new CancellationToken();
        [System.NonSerialized] private int invokeCounter;

        [ContextMenu("Invoke")]
        public void DoInvoke()
        {
            host.scheduler.Invoke(ScheduledInvoke, 5f, this, null, token);
        }

        [ContextMenu("Invoke Repeating")]
        public void DoInvokeRepeating()
        {
            host.scheduler.InvokeRepeating(ScheduledInvoke, 1f, 5f, this, null, token);
        }

        [ContextMenu("Cancel Invoke")]
        public void DoCancelInvoke()
        {
            token.Cancel();
            token = new CancellationToken();
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
    }
}
