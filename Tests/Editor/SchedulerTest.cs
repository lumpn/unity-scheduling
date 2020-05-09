//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//----------------------------------------
using NUnit.Framework;

namespace Lumpn.Scheduling
{
    [TestFixture]
    public sealed class SchedulerTest
    {
        private sealed class Counter
        {
            public int value;
        }

        private sealed class Operands
        {
            public readonly int a, b;
            public int result;

            public Operands(int a, int b)
            {
                this.a = a;
                this.b = b;
            }
        }

        [Test]
        public void TestUpdate()
        {
            var v1 = new Operands(3, 5);
            var v2 = new Operands(7, 2);
            Assert.AreEqual(0, v1.result);
            Assert.AreEqual(0, v2.result);

            var scheduler = new Scheduler(0f, 20);
            scheduler.Invoke(ComputeProduct, 3, this, v1);
            scheduler.Invoke(ComputeProduct, 5, this, v2);
            Assert.AreEqual(2, scheduler.QueueLength);
            Assert.AreEqual(0, v1.result);
            Assert.AreEqual(0, v2.result);

            scheduler.Update(4f);
            Assert.AreEqual(1, scheduler.QueueLength);
            Assert.AreEqual(15, v1.result);
            Assert.AreEqual(0, v2.result);

            scheduler.Update(6f);
            Assert.AreEqual(0, scheduler.QueueLength);
            Assert.AreEqual(15, v1.result);
            Assert.AreEqual(14, v2.result);
        }

        [Test]
        public void TestCancellation()
        {
            var v1 = new Operands(3, 5);
            var v2 = new Operands(7, 2);
            Assert.AreEqual(0, v1.result);
            Assert.AreEqual(0, v2.result);

            var token = new CancellationToken();
            var scheduler = new Scheduler(0f, 20);
            scheduler.Invoke(ComputeProduct, 3, this, v1, token);
            scheduler.Invoke(ComputeProduct, 5, this, v2, token);
            Assert.AreEqual(2, scheduler.QueueLength);
            Assert.AreEqual(0, v1.result);
            Assert.AreEqual(0, v2.result);

            token.Cancel();
            scheduler.Update(6f);
            Assert.AreEqual(0, scheduler.QueueLength);
            Assert.AreEqual(0, v1.result);
            Assert.AreEqual(0, v2.result);
        }

        [Test]
        public void TestInvokeRepeating()
        {
            var counter = new Counter();
            Assert.AreEqual(0, counter.value);

            var scheduler = new Scheduler(0f, 20);
            scheduler.InvokeRepeating(IncrementCounter, 1, 2, this, counter);
            Assert.AreEqual(1, scheduler.QueueLength);
            Assert.AreEqual(0, counter.value);

            scheduler.Update(2f);
            Assert.AreEqual(1, scheduler.QueueLength);
            Assert.AreEqual(1, counter.value);

            scheduler.Update(4f);
            Assert.AreEqual(1, scheduler.QueueLength);
            Assert.AreEqual(2, counter.value);
        }

        private static void IncrementCounter(object owner, object state)
        {
            var counter = (Counter)state;
            counter.value++;
        }

        private static void ComputeProduct(object owner, object state)
        {
            var operands = (Operands)state;
            operands.result = operands.a * operands.b;
        }
    }
}
