using System;
using System.IO;

class Program
{

    /// <summary>
    /// This program generates the Fibonacci sequence up to a specified count and writes it to a file.
    /// The first two Fibonacci numbers are initialized.
    /// A loop calculates the next numbers in the sequence.
    /// The array is converted into a comma-separated string.
    /// The string is written to a file using File.WriteAllText().
    /// The first two Fibonacci numbers are initialized.
    /// </summary>
    static void Main()
    {
        string fileName = "Fibonacci.txt";
        int count = 15;
        int[] fibonacci = new int[count];

        // Generate Fibonacci sequence
        fibonacci[0] = 0;
        fibonacci[1] = 1;

        for (int i = 2; i < count; i++)
        {
            fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];
        }

        // Convert to comma-separated string
        string output = string.Join(",", fibonacci);

        // Write to file
        try
        {
            File.WriteAllText(fileName, output);
            Console.WriteLine($"Fibonacci sequence saved to {fileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }
}