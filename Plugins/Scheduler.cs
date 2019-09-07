//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn
{
    public sealed class Scheduler
    {
        public delegate void Method(object owner, object state);

        private sealed class EntryComparer : IComparer<Entry>
        {
            private static readonly Comparer<float> timeComparer = Comparer<float>.Default;

            public int Compare(Entry a, Entry b)
            {
                return timeComparer.Compare(a.time, b.time);
            }
        }

        private struct Entry
        {
            private readonly ICancellationToken token;
            private readonly Method method;
            private readonly object owner;
            private readonly object state;
            public readonly float time;
            private readonly float repeatInterval;

            public Entry(ICancellationToken token, Method method, object owner, object state, float time, float repeatInterval)
            {
                this.token = token;
                this.method = method;
                this.owner = owner;
                this.state = state;
                this.time = time;
                this.repeatInterval = repeatInterval;
            }

            public void Invoke(Scheduler scheduler)
            {
                if (token != null && token.isCanceled) return;
                SafeInvoke();

                if (repeatInterval < 0f) return;
                scheduler.InvokeRepeating(method, repeatInterval, repeatInterval, owner, state, token);
            }

            private void SafeInvoke()
            {
                try
                {
                    method(owner, state);
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        private static readonly EntryComparer entryComparer = new EntryComparer();
        private readonly Heap<Entry> entries;
        private float time;

        public int QueueLength { get { return entries.Count; } }

        public Scheduler(float time, int initialCapacity)
        {
            this.entries = new Heap<Entry>(entryComparer, initialCapacity);
            this.time = time;
        }

        public void Update(float time)
        {
            this.time = time;

            Entry top;
            while (TryDequeue(entries, time, out top))
            {
                top.Invoke(this);
            }
        }

        private static bool TryDequeue(Heap<Entry> entries, float time, out Entry entry)
        {
            if (entries.Count < 1)
            {
                entry = default(Entry);
                return false;
            }

            var top = entries.Peek();
            if (top.time > time)
            {
                entry = default(Entry);
                return false;
            }

            entry = entries.Dequeue();
            return true;
        }

        public void Invoke(Method method, float delaySeconds, object owner = null, object state = null, ICancellationToken token = null)
        {
            InvokeRepeating(method, delaySeconds, -1f, owner, state, token);
        }

        public void InvokeRepeating(Method method, float delaySeconds, float repeatInterval, object owner = null, object state = null, ICancellationToken token = null)
        {
            Debug.Assert(method != null, "method must not be null");

            var entry = new Entry(token, method, owner, state, time + delaySeconds, repeatInterval);
            entries.Enqueue(entry);
        }
    }
}
