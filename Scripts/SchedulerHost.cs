//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn
{
    public sealed class SchedulerHost : MonoBehaviour
    {
        public static SchedulerHost main;

        [SerializeField] private int initialCapacity = 1000;

        [field: System.NonSerialized] public Scheduler scheduler { get; private set; }

        void Start()
        {
            scheduler = new Scheduler(Time.time, initialCapacity);
            main = this;
        }

        void Update()
        {
            scheduler.Update(Time.time);
        }
    }
}
