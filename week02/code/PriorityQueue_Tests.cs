using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue from empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty.".
    // Defect(s) Found: None, just checking that the queue is not empty.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();
        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Single person enqueue/dequeue.
    // Expected Result: Person is returned and removed.
    // Defect(s) Found: Code does not remove the item. Need to add _queue.RemoveAt() to remove the person.
    public void TestPriorityQueue_SinglePerson()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Bob", 1);
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Bob", result);
        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Multiple items, highest priority removed.
    // Expected Result: Person with highest priority returned.
    // Defect(s) Found: Original code skipped last element because of the range. Need to remove the -1 from the for loop in the dequeue function.
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Bob", 1);
        priorityQueue.Enqueue("Tim", 3);
        priorityQueue.Enqueue("Sue", 2);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Tim", result);
    }

    [TestMethod]
    // Scenario: Multiple items with same highest priority.
    // Expected Result: First one (FIFO) with same high priority removed.
    // Defect(s) Found: Code removes the last item due to a wrong validation ">=". It should be ">".
    public void TestPriorityQueue_FifoPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Bob", 5);
        priorityQueue.Enqueue("Tim", 5);
        priorityQueue.Enqueue("Sue", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Bob", result);
    }
}
