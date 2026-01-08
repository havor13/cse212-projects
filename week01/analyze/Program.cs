using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("====================================");
        Console.WriteLine(" Week 01 Analyze: Performance Runner ");
        Console.WriteLine("====================================");
        Console.WriteLine("Choose which part to run:");
        Console.WriteLine("1 - Sorting (SortArray)");
        Console.WriteLine("2 - Standard Deviation (3 implementations)");
        Console.WriteLine("3 - Search (SearchSorted1 vs SearchSorted2)");
        Console.Write("Enter choice: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Sorting.Run();
                break;
            case "2":
                StandardDeviation.Run();
                break;
            case "3":
                Search.Run();
                break;
            default:
                Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                break;
        }
    }
}
