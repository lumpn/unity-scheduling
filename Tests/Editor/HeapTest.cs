//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using NUnit.Framework;

namespace Lumpn.Scheduling
{
    [TestFixture]
    public sealed class HeapTest
    {
        [Test]
        public void TestPushPop()
        {
            var heap = new Heap<string>(System.StringComparer.Ordinal, 20);
            heap.Push("Hello");
            heap.Push("World");
            heap.Push("Foo");
            heap.Push("Bar");
            heap.Push("Baz");
            Assert.AreEqual(5, heap.Count);

            var e1 = heap.Pop();
            var e2 = heap.Pop();
            var e3 = heap.Pop();
            Assert.AreEqual(2, heap.Count);
            Assert.AreEqual("Bar", e1);
            Assert.AreEqual("Baz", e2);
            Assert.AreEqual("Foo", e3);
        }

        [Test]
        public void TestPeek()
        {
            var heap = new Heap<string>(System.StringComparer.Ordinal, 20);
            heap.Push("Foo");
            heap.Push("Bar");
            heap.Push("Baz");
            Assert.AreEqual(3, heap.Count);

            var p1 = heap.Peek();
            var p2 = heap.Peek();
            Assert.AreEqual(3, heap.Count);
            Assert.AreEqual("Bar", p1);
            Assert.AreEqual("Bar", p2);

            var p3 = heap.Pop();
            Assert.AreEqual(2, heap.Count);
            Assert.AreEqual("Bar", p3);
        }

        [Test]
        public void TestPushRange()
        {
            var heap = new Heap<string>(System.StringComparer.Ordinal, 20);
            heap.Push(new[] { "Hello", "World", "Foo", "Bar", "Baz" });
            Assert.AreEqual(5, heap.Count);

            var e1 = heap.Pop();
            var e2 = heap.Pop();
            var e3 = heap.Pop();
            Assert.AreEqual(2, heap.Count);
            Assert.AreEqual("Bar", e1);
            Assert.AreEqual("Baz", e2);
            Assert.AreEqual("Foo", e3);
        }
    }
}
