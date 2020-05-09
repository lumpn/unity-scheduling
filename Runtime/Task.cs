//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.Scheduling
{
    public struct Task
    {
        private readonly ICancellationToken token;
        private readonly Callback callback;
        private readonly object owner;
        private readonly object state;
        private readonly float repeatInterval;
        public readonly float time;

        public Task(ICancellationToken token, Callback callback, object owner, object state, float time, float repeatInterval)
        {
            this.token = token;
            this.callback = callback;
            this.owner = owner;
            this.state = state;
            this.time = time;
            this.repeatInterval = repeatInterval;
        }

        public void Invoke(Scheduler scheduler)
        {
            if (token != null && token.isCanceled) return;

            try
            {
                callback(owner, state);
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }

            if (repeatInterval < 0f) return;
            scheduler.InvokeRepeating(callback, repeatInterval, repeatInterval, owner, state, token);
        }
    }
}
