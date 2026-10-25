using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        List<string> products = new List<string>();

        Console.WriteLine("=== Product List Manager ===");
        Console.WriteLine("Write products like CE-400");
        Console.WriteLine("Write EXIT to finish");
        Console.WriteLine();

        while (true)
        {
            Console.Write("Enter product: ");
            string input = Console.ReadLine() ?? "";

            // remove spaces
            string clean = input.Trim();

            // check for exit in any case
            if (clean.ToLower() == "exit")
            {
                break;
            }

            // check if valid
            if (CheckProduct(clean) == true)
            {
                products.Add(clean.ToUpper());
                Console.WriteLine("Added!");
            }
            else
            {
                Console.WriteLine("Invalid! Example valid: CE-400, LABAN-231");
                Console.WriteLine("Invalid examples: CE400, LABAN-100");
            }
        }

        // sorting and showing
        Console.WriteLine();
        Console.WriteLine("--- Final List ---");

        if (products.Count == 0)
        {
            Console.WriteLine("No products");
        }
        else
        {
            products.Sort();

            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + products[i]);
            }

            Console.WriteLine("Total: " + products.Count);
        }
    }

    // method to check product
    static bool CheckProduct(string name)
    {
        // check if empty
        if (name == "" || name == null)
        {
            return false;
        }

        // check format LETTERS-NUMBER
        if (Regex.IsMatch(name, @"^[A-Za-z]+-\d+$") == false)

        {
            return false;
        }

        // split into two parts
        string[] parts = name.Split('-');
        string numberPart = parts[1];

        int number;
        bool isNumber = int.TryParse(numberPart, out number);

        if (isNumber == false)
        {
            return false;
        }

        // number must be between 200 and 500
        if (number < 200)
        {
            return false;
        }

        if (number > 500)
        {
            return false;
        }

        return true;
    }
}