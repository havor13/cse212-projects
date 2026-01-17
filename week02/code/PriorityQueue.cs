using System;
using System.Collections.Generic;

public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority.
    /// The node is always added to the back of the queue regardless of priority.
    /// </summary>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode);
    }

    /// <summary>
    /// Remove the item with the highest priority and return its value.
    /// If multiple items share the highest priority, the earliest one (FIFO) is removed.
    /// If the queue is empty, throws InvalidOperationException with message "The queue is empty."
    /// </summary>
    public string Dequeue()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        int highPriorityIndex = 0;
        for (int index = 1; index < _queue.Count; index++) // include last item
        {
            // Strictly greater preserves FIFO for ties
            if (_queue[index].Priority > _queue[highPriorityIndex].Priority)
            {
                highPriorityIndex = index;
            }
        }

        string value = _queue[highPriorityIndex].Value;
        _queue.RemoveAt(highPriorityIndex); // actually remove
        return value;
    }

    // DO NOT MODIFY
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    // DO NOT MODIFY
    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}
