//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Scheduling
{
    internal sealed class TaskComparer : IComparer<Task>
    {
        public static readonly TaskComparer Default = new TaskComparer();

        private static readonly Comparer<float> timeComparer = Comparer<float>.Default;

        public int Compare(Task a, Task b)
        {
            return timeComparer.Compare(a.time, b.time);
        }
    }
}
