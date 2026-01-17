using System;

public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add a person to the queue with a name and number of turns.
    /// Always enqueue to the back of the queue (FIFO order).
    /// </summary>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person in the queue.
    /// - If turns > 1: decrement and re-enqueue.
    /// - If turns == 1: remove permanently.
    /// - If turns <= 0: infinite turns, re-enqueue.
    /// - If queue is empty: throw InvalidOperationException.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        // Instead of re-enqueuing immediately, adjust turns first
        if (person.Turns > 1)
        {
            person.Turns -= 1;
            // ✅ Place at the back, but after any newly added players
            _people.Enqueue(person);
        }
        else if (person.Turns <= 0) // infinite turns
        {
            _people.Enqueue(person);
        }
        // If Turns == 1 → do nothing (removed permanently)

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}
