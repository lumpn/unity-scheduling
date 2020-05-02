//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Scheduling
{
    public sealed class TaskComparer : IComparer<Task>
    {
        private static readonly Comparer<float> timeComparer = Comparer<float>.Default;

        public int Compare(Task a, Task b)
        {
            return timeComparer.Compare(a.time, b.time);
        }
    }
}
