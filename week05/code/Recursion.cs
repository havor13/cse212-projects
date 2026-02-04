using System.Collections;
using System.Diagnostics;

public static class Recursion
{
    /// <summary>
    /// Problem 1: Recursive Squares Sum
    /// Using recursion, find the sum of 1^2 + 2^2 + ... + n^2.
    /// Base case: if n <= 0, return 0.
    /// Recursive case: n^2 + SumSquaresRecursive(n-1).
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// Problem 2: Permutations Choose
    /// Generate permutations of length 'size' from given letters.
    /// Base case: when word length == size, add to results.
    /// Recursive case: choose each letter, recurse with remaining.
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            var remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + letters[i]);
        }
    }

    /// <summary>
    /// Problem 3: Climbing Stairs
    /// Count ways to climb s stairs with steps of 1, 2, or 3.
    /// Use memoization to avoid recomputation.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        if (s == 0) return 0;
        if (s == 1) return 1;
        if (s == 2) return 2;
        if (s == 3) return 4;

        if (remember.ContainsKey(s))
            return remember[s];

        decimal ways = CountWaysToClimb(s - 1, remember)
                     + CountWaysToClimb(s - 2, remember)
                     + CountWaysToClimb(s - 3, remember);

        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// Problem 4: Wildcard Binary Patterns
    /// Expand binary string with '*' wildcards into all possible strings.
    /// Base case: no '*' left, add pattern to results.
    /// Recursive case: replace '*' with '0' and '1'.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        WildcardBinary(pattern.Substring(0, index) + "0" + pattern.Substring(index + 1), results);
        WildcardBinary(pattern.Substring(0, index) + "1" + pattern.Substring(index + 1), results);
    }

    /// <summary>
    /// Problem 5: Maze
    /// Use recursion to insert all paths from (0,0) to the end square.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        if (currPath == null)
            currPath = new List<ValueTuple<int, int>>();

        currPath.Add((x, y));

        // Base case: reached the end
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Explore directions (down, up, right, left)
        int[,] directions = { {1,0}, {-1,0}, {0,1}, {0,-1} };
        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newX = x + directions[i,0];
            int newY = y + directions[i,1];

            if (maze.IsValidMove(currPath, newX, newY))
            {
                SolveMaze(results, maze, newX, newY, new List<ValueTuple<int, int>>(currPath));
            }
        }

        currPath.RemoveAt(currPath.Count - 1);
    }
}
