
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        List<string> products = new List<string>();
        Console.WriteLine("=== Product List Manager ===");
        Console.WriteLine("Enter products like CE-400 (type EXIT to finish)\n");

        while (true)

        {
            Console.Write("Enter product: ");
            string input = Console.ReadLine();

            // Level 2: Improved Exit Handling
            string cleanedInput = input.Trim();

            if (cleanedInput.ToLower() == "exit")
            {
                break;
            }

            // Level 3: Validation
            if (IsValidProduct(cleanedInput, out string errorMessage))
            {
                // نخزنها بحروف كبيرة ومرتبة
                products.Add(cleanedInput.ToUpper());
                Console.WriteLine($"-> Added: {cleanedInput.ToUpper()}");
            }
            else
            {
                Console.WriteLine($"-> Invalid! {errorMessage}");
            }
        }

        // Level 1 + 2: Display sorted
        DisplayProducts(products);
    }

    // Level 3: Method + Regex + TryParse + Defensive Programming
    static bool IsValidProduct(string product, out string error)
    {
        error = "";

        if (string.IsNullOrWhiteSpace(product))
        {
            error = "Name cannot be empty.";
            return false;
        }

        // Regex: حروف فقط - رقم فقط
        // مثال: LABAN-231
        string pattern = @"^[A-Za-z]+-\d+$";
        if (!Regex.IsMatch(product, pattern))
        {
            error = "Format must be LETTERS-NUMBER (e.g. CE-400, LABAN-231). Examples of invalid: CE400, XX3-480";
            return false;
        }

        // نقسمه لقسمين
        string[] parts = product.Split('-');
        string lettersPart = parts[0];
        string numbersPart = parts[1];

        // Left side: Letters only (Regex already checked, but for clean code)
        if (!lettersPart.All(char.IsLetter))
        {
            error = "Left side must be letters only A-Z.";
            return false;
        }

        // Right side: Numbers + range 200-500
        if (!int.TryParse(numbersPart, out int number))
        {
            error = "Right side must be numbers only.";
            return false;
        }

        if (number < 200 || number > 500)
        {
            error = $"Number must be between 200 and 500. You entered {number}. Example invalid: LABAN-100";
            return false;
        }

        return true;
    }

    // Clean Code Principle: Method for display
    static void DisplayProducts(List<string> products)
    {
        Console.WriteLine("\n--- Final Result ---");

        if (products.Count == 0)
        {
            Console.WriteLine("No products entered.");
            return;
        }

        // Level 2: Sorting
        products.Sort();

        Console.WriteLine($"You entered {products.Count} valid products (sorted A-Z):");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i]}");
        }
    }
}