using System;
using System.Collections.Generic;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  
    /// For example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  
    /// Assume that length is a positive integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Plan:
        // 1. Create a new array of size 'length' to hold the multiples.
        // 2. Loop from i = 0 up to i < length.
        // 3. For each index i, compute number * (i + 1).
        //    - Example: number = 7, length = 5 → {7*1, 7*2, 7*3, 7*4, 7*5} → {7,14,21,28,35}.
        // 4. Store the result in the array at position i.
        // 5. After the loop, return the filled array.

        double[] result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  
    /// For example, if the data is List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and amount = 3,  
    /// the list after the function runs should be List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  
    /// The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Plan:
        // 1. Find the total number of elements in the list (count).
        // 2. Slice the last 'amount' elements using GetRange(count - amount, amount).
        // 3. Remove those elements from the original list using RemoveRange(count - amount, amount).
        // 4. Insert the sliced elements at the front using InsertRange(0, slice).
        // 5. The list 'data' is now rotated in-place.

        int count = data.Count;

        // Step 2: Slice the last 'amount' elements
        List<int> tail = data.GetRange(count - amount, amount);

        // Step 3: Remove those elements from the original list
        data.RemoveRange(count - amount, amount);

        // Step 4: Insert them at the front
        data.InsertRange(0, tail);
    }
}
