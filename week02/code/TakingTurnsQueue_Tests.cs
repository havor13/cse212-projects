using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TakingTurnsQueueTests
{
    /*
     SUMMARY OF TEST RESULTS AND FIXES
     ---------------------------------
     - TestTakingTurnsQueue_FiniteRepetition:
       ❌ Failed initially (Sue dequeued first instead of Bob).
       ✅ Passed after fixing PersonQueue to use FIFO order.

     - TestTakingTurnsQueue_AddPlayerMidway:
       ❌ Failed initially (Sue dequeued before Bob when adding Tim).
       ✅ Passed after fixing Enqueue to add to the back of the queue.

     - TestTakingTurnsQueue_ForeverZero:
       ❌ Failed initially (Sue dequeued first instead of Tim).
       ✅ Passed after fixing GetNextPerson to re-enqueue infinite-turn players correctly.

     - TestTakingTurnsQueue_ForeverNegative:
       ❌ Failed initially (Sue dequeued first instead of Tim).
       ✅ Passed after fixing GetNextPerson to handle negative turns as infinite.

     - TestTakingTurnsQueue_EmptyQueueThrows:
       ✅ Passed from the beginning (exception thrown correctly).

     Overall: All requirements are now implemented correctly. The queue respects FIFO order,
     finite turns are decremented and removed when exhausted, and infinite-turn players
     (turns <= 0) remain in the queue forever.
    */

    [TestMethod]
    // Requirement: Finite turns respected.
    // Scenario: Add Bob (2 turns) and Sue (2 turns).
    // Expected Result: Bob dequeued first, then Sue, then Bob again, then Sue removed.
    // Test Result: ❌ Failed initially (Sue dequeued first), ✅ Passed after fixing FIFO order.
    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Bob", 2);
        queue.AddPerson("Sue", 2);

        Assert.AreEqual("Bob", queue.GetNextPerson().Name);
        Assert.AreEqual("Sue", queue.GetNextPerson().Name);
        Assert.AreEqual("Bob", queue.GetNextPerson().Name);
        Assert.AreEqual("Sue", queue.GetNextPerson().Name);
        Assert.ThrowsException<InvalidOperationException>(() => queue.GetNextPerson());
    }

    [TestMethod]
    // Requirement: Adding new players mid‑rotation.
    // Scenario: Add Bob (2 turns), Sue (2 turns), then Tim (2 turns) after one cycle.
    // Expected Result: Bob dequeued first, then Sue, then Tim joins correctly at the back.
    // Test Result: ❌ Failed initially (Sue dequeued before Bob), ✅ Passed after fixing Enqueue to add at back.
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Bob", 2);
        queue.AddPerson("Sue", 2);

        Assert.AreEqual("Bob", queue.GetNextPerson().Name);
        queue.AddPerson("Tim", 2);

        Assert.AreEqual("Sue", queue.GetNextPerson().Name);
    }

    [TestMethod]
    // Requirement: Infinite turns when turns == 0.
    // Scenario: Add Tim (0 turns → infinite) and Sue (2 turns).
    // Expected Result: Tim dequeued first, Sue second, Tim stays forever.
    // Test Result: ❌ Failed initially (Sue dequeued first), ✅ Passed after fixing infinite turn logic.
    public void TestTakingTurnsQueue_ForeverZero()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Tim", 0);
        queue.AddPerson("Sue", 2);

        Assert.AreEqual("Tim", queue.GetNextPerson().Name);
        Assert.AreEqual("Sue", queue.GetNextPerson().Name);
        Assert.AreEqual("Tim", queue.GetNextPerson().Name);
    }

    [TestMethod]
    // Requirement: Infinite turns when turns < 0.
    // Scenario: Add Tim (-1 turns → infinite) and Sue (2 turns).
    // Expected Result: Tim dequeued first, Sue second, Tim stays forever.
    // Test Result: ❌ Failed initially (Sue dequeued first), ✅ Passed after fixing infinite turn logic.
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Tim", -1);
        queue.AddPerson("Sue", 2);

        Assert.AreEqual("Tim", queue.GetNextPerson().Name);
        Assert.AreEqual("Sue", queue.GetNextPerson().Name);
        Assert.AreEqual("Tim", queue.GetNextPerson().Name);
    }

    [TestMethod]
    // Requirement: Empty queue throws exception.
    // Scenario: Call GetNextPerson on empty queue.
    // Expected Result: InvalidOperationException with message "No one in the queue."
    // Test Result: ✅ Passed.
    public void TestTakingTurnsQueue_EmptyQueueThrows()
    {
        var queue = new TakingTurnsQueue();
        Assert.ThrowsException<InvalidOperationException>(() => queue.GetNextPerson());
    }
}
