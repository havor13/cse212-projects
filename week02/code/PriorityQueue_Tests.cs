using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Requirement: Higher priority dequeued first.
    // Scenario: Enqueue "Low"(1) then "High"(5).
    // Expected: Dequeue returns "High".
    // Test Result: ❌ Failed initially (returned "Low"); ✅ Passed after fixing Dequeue loop and removal.
    public void Test_HighPriorityDequeuedFirst()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("Low", 1);
        pq.Enqueue("High", 5);

        Assert.AreEqual("High", pq.Dequeue());
    }

    [TestMethod]
    // Requirement: FIFO when priorities are equal.
    // Scenario: Enqueue "First"(3) then "Second"(3).
    // Expected: Dequeue returns "First".
    // Test Result: ❌ Failed initially (returned "Second"); ✅ Passed after using strict '>' comparison.
    public void Test_EqualPriorityFIFO()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("First", 3);
        pq.Enqueue("Second", 3);

        Assert.AreEqual("First", pq.Dequeue());
    }

    [TestMethod]
    // Requirement: Mixed priorities handled correctly.
    // Scenario: Enqueue "Low"(1), "Medium"(3), "High"(5).
    // Expected: Dequeue order: High → Medium → Low.
    // Test Result: ❌ Failed initially (queue never shrank); ✅ Passed after RemoveAt(highPriorityIndex).
    public void Test_MixedPriorities()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("Low", 1);
        pq.Enqueue("Medium", 3);
        pq.Enqueue("High", 5);

        Assert.AreEqual("High", pq.Dequeue());
        Assert.AreEqual("Medium", pq.Dequeue());
        Assert.AreEqual("Low", pq.Dequeue());
    }

    [TestMethod]
    // Requirement: Empty queue throws specific exception.
    // Scenario: Dequeue on empty queue.
    // Expected: InvalidOperationException with message "The queue is empty."
    // Test Result: ✅ Passed.
    [ExpectedException(typeof(InvalidOperationException))]
    public void Test_EmptyQueueThrows()
    {
        var pq = new PriorityQueue();
        pq.Dequeue();
    }
}
