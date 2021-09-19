//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.Scheduling.Demo
{
    public sealed class SchedulerHost : MonoBehaviour
    {
        public readonly Scheduler scheduler = new Scheduler(0, 1000);

        void Update()
        {
            scheduler.Update(Time.time);
        }
    }
}
