//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.Scheduling
{
    public sealed class Scheduler
    {
        private static readonly TaskComparer entryComparer = new TaskComparer();

        private readonly Heap<Task> entries;
        private float time;

        public int QueueLength { get { return entries.Count; } }

        public Scheduler(float time, int initialCapacity)
        {
            this.entries = new Heap<Task>(entryComparer, initialCapacity);
            this.time = time;
        }

        public void Update(float time)
        {
            this.time = time;

            while (TryDequeue(entries, time, out Task topTask))
            {
                topTask.Invoke(this);
            }
        }

        private static bool TryDequeue(Heap<Task> entries, float time, out Task entry)
        {
            if (entries.Count < 1)
            {
                entry = default(Task);
                return false;
            }

            var top = entries.Peek();
            if (top.time > time)
            {
                entry = default(Task);
                return false;
            }

            entry = entries.Pop();
            return true;
        }

        public void Invoke(Callback callback, float delaySeconds, object owner = null, object state = null, ICancellationToken token = null)
        {
            InvokeRepeating(callback, delaySeconds, -1f, owner, state, token);
        }

        public void InvokeRepeating(Callback callback, float delaySeconds, float repeatInterval, object owner = null, object state = null, ICancellationToken token = null)
        {
            Debug.Assert(callback != null, "callback must not be null");

            var entry = new Task(token, callback, owner, state, time + delaySeconds, repeatInterval);
            entries.Push(entry);
        }
    }
}
