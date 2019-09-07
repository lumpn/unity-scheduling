//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace Lumpn
{
    /// Minimum heap
    public sealed class Heap<T>
    {
        private readonly IComparer<T> comparer;
        private readonly List<T> heap;

        public int Count
        {
            get { return heap.Count; }
        }

        public int LastIndex
        {
            get { return Count - 1; }
        }

        public int Capacity
        {
            get { return heap.Capacity; }
            set { heap.Capacity = value; }
        }

        public Heap(IComparer<T> comparer, int initialCapacity)
        {
            this.comparer = comparer;
            this.heap = new List<T>(initialCapacity);
        }

        public void Enqueue(IEnumerable<T> items)
        {
            int prevCount = Count;
            heap.AddRange(items);

            // heapify
            for (int i = prevCount; i < Count; i++)
            {
                BubbleUp(i);
            }
        }

        public void Enqueue(T item)
        {
            heap.Add(item);
            BubbleUp(LastIndex);
        }

        public T Dequeue()
        {
            T result = heap[0];
            Swap(LastIndex, 0);
            heap.RemoveAt(LastIndex);
            BubbleDown(0);
            return result;
        }

        public T Peek()
        {
            return heap[0];
        }

        private void BubbleUp(int i)
        {
            if (i == 0) return;

            var parent = Parent(i);
            if (comparer.Compare(heap[i], heap[parent]) >= 0) return;

            Swap(i, parent);
            BubbleUp(parent);
        }

        private void BubbleDown(int i)
        {
            var childA = FirstChild(i);
            var childB = childA + 1;
            if (childA >= Count) return; // no children

            var minChild = (childB >= Count || comparer.Compare(heap[childA], heap[childB]) < 0) ? childA : childB;
            if (comparer.Compare(heap[minChild], heap[i]) >= 0) return;

            Swap(i, minChild);
            BubbleDown(minChild);
        }

        private void Swap(int i, int j)
        {
            T tmp = heap[i];
            heap[i] = heap[j];
            heap[j] = tmp;
        }

        private static int Parent(int i)
        {
            return (i + 1) / 2 - 1;
        }

        private static int FirstChild(int i)
        {
            return (i + 1) * 2 - 1;
        }

        public void Clear()
        {
            heap.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return heap.GetEnumerator();
        }

        public void DebugPrint()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in heap)
            {
                sb.Append(item);
                sb.Append(", ");
            }
            Debug.Log(sb.ToString());
        }
    }
}
