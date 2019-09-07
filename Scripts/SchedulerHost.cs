using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn
{
    public sealed class SchedulerHost : MonoBehaviour
    {
        public static SchedulerHost main;

        [SerializeField] private int initialCapacity = 1000;

        [field: System.NonSerialized] public Scheduler scheduler { get; private set; }

        void Awake()
        {
            var time = Time.time;
            scheduler = new Scheduler(time, initialCapacity);
            main = this;
        }

        void Update()
        {
            var time = Time.time;
            scheduler.Update(time);
        }
    }
}
