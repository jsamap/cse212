using System.Diagnostics;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN: 
        // Step 1. Create an array the size of the length parameter.
        // Step 2. Create a FOR loop starting from 1 and ending at the length value, to iterate through the recently created array.. 
        // Step 3. Multiply the value of the iteration variable by the number value (first parameter) in each iteration. 
        // Step 4. Save each result in the array created using the iteration variable as the index of the array.

        var doubles = new double[length];

        for (int i=1; i<=length; i++)
            doubles[i-1] = i*number;

        Debug.WriteLine($"Number: {number} | Length: {length}");
        Debug.WriteLine(String.Join(" | ", doubles));
        
        return doubles;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        
        // PLAN: 
        // Step 1. Take the last n items of the list (n being the amount value) and copy them to a temporal list.
        // Step 2. Remove the copied items from the end of the list. 
        // Step 3. Then, insert the temporal list at the beginning of the original list in the same order.
        // This logic will take any amount of items at the end of the list and will place them at the beginning of the list in the same order.
        // Note: Since the larger number is data.Count, there is no need to use mod.

        Debug.WriteLine($"Amount: {amount}");
        Debug.WriteLine(String.Join(" | ",data));

        List<int> rotation = data.GetRange(data.Count-amount, amount);
        data.RemoveRange(data.Count-amount, amount);
        data.InsertRange(0, rotation);

        Debug.WriteLine(String.Join(" | ",data));
        
    }
}
