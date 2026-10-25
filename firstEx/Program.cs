using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // List to store all valid products
        List<string> products = new List<string>();

        Console.WriteLine("=== Product List Manager ===");
        Console.WriteLine("Write like CE-400 (number 200 to 500)");
        Console.WriteLine("Write EXIT to finish\n");

        while (true)
        {
            Console.Write("Enter product: ");
            string input = Console.ReadLine();

            if (input == null) continue;

            // Remove spaces from start and end

            string clean = input.Trim();

            // Check if user wants to exit (case insensitive)
            if (clean.ToUpper() == "EXIT")
            {
                break;
            }

            // Validate the product
            if (IsValidProduct(clean))
            {
                // Convert to uppercase and add to list
                products.Add(clean.ToUpper());
                Console.WriteLine("Added!");
            }
            else
            {
                Console.WriteLine("Invalid! Use LETTERS-NUMBER like CE-400");
            }
        }

        // Show final results
        Console.WriteLine("\n--- Final List ---");
        if (products.Count == 0)
        {
            Console.WriteLine("No products");
        }
        else
        {
            // Sort alphabetically
            products.Sort();

            // Print with numbers 1, 2, 3...
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + products[i]);
            }
            Console.WriteLine("Total: " + products.Count);
        }
    }

    // Simple validation without Regex - perfect for beginners
    static bool IsValidProduct(string name)
    {
        // 1. Check if empty or only spaces
        if (string.IsNullOrWhiteSpace(name)) return false;

        // 2. Must contain a dash
        if (!name.Contains("-")) return false;

        // Split into two parts: e.g. "CE-400" -> ["CE", "400"]
        string[] parts = name.Split('-');

        // Must have exactly 2 parts
        if (parts.Length != 2) return false;

        string letters = parts[0];
        string numbers = parts[1];

        // 3. First part must be only letters and not empty
        if (letters.Length == 0) return false;
        foreach (char c in letters)
        {
            if (!char.IsLetter(c)) return false;
        }

        // 4. Second part must be only digits and not empty
        if (numbers.Length == 0) return false;
        foreach (char c in numbers)
        {
            if (!char.IsDigit(c)) return false;
        }

        // 5. Number must be between 200 and 500
        int num = int.Parse(numbers);
        if (num < 200 || num > 500)
        {
            return false;
        }

        // All checks passed
        return true;
    }
}