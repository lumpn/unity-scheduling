//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.Scheduling
{
    public sealed class SchedulerHost : MonoBehaviour
    {
        [SerializeField] private int initialCapacity = 1000;

        [field: System.NonSerialized] public Scheduler scheduler { get; private set; }

        void Start()
        {
            scheduler = new Scheduler(Time.time, initialCapacity);
        }

        void Update()
        {
            scheduler.Update(Time.time);
        }
    }
}
