//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//----------------------------------------
using UnityEngine;
using UnityEngine.Profiling;

namespace Lumpn.Scheduling
{
    public class InvokeDemo : MonoBehaviour
    {
        [SerializeField] private SchedulerHost host;
        [SerializeField] private float invokeDelay = 3f;
        [SerializeField] private float repeatDelay = 1f;
        [SerializeField] private float repeatInterval = 5f;

        [System.NonSerialized] private CancellationToken token = new CancellationToken();
        [System.NonSerialized] private int invokeCounter;

        public int InvokeCounter { get { return invokeCounter; } }

        [ContextMenu("Invoke")]
        public void DoInvoke()
        {
            Profiler.BeginSample("DoInvoke");
            host.scheduler.Invoke(ScheduledInvoke, invokeDelay, this, null, token);
            Profiler.EndSample();
        }

        [ContextMenu("Invoke Repeating")]
        public void DoInvokeRepeating()
        {
            Profiler.BeginSample("DoInvokeRepeating");
            host.scheduler.InvokeRepeating(ScheduledInvoke, repeatDelay, repeatInterval, this, null, token);
            Profiler.EndSample();
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
