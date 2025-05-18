using System;

class Program
{

    /// <summary>
    /// This program calculates the sum of all multiples of 3 or 5 below a given limit.
    /// 1. It checks for a valid positive integer input.
    /// 2. It formats the output exactly as requested(numbers joined with + followed by = sum).
    /// </summary>
    static void Main()
    {
        Console.Write("Enter an integer: ");
        if (int.TryParse(Console.ReadLine(), out int limit) && limit > 0)
        {
            int sum = 0;
            string result = "";

            for (int i = 1; i <= limit; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                    result += (result == "") ? $"{i}" : $"+{i}";
                }
            }

            Console.WriteLine($"{result} = {sum}");
        }
        else
        {
            Console.WriteLine("Please enter a valid positive integer.");
        }
    }
}
