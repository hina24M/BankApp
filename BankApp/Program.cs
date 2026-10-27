using System;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Create Premium Bank Account ---");

            // Customer details input using validation loops
            string name = GetValidCustomerName();
            string sortCode = GetValidSortCode();
            string accountNumber = GetValidAccountNumber();

            // Create account object
            PremiumAccount account = new PremiumAccount(name, sortCode, accountNumber);

            // Initial deposit with clear letter/number checking
            decimal deposit = GetValidDeposit();
            account.Balance = deposit;

            // Process 3 transactions with clear letter/number checking
            for (int i = 1; i <= 3; i++)
            {
                Console.Write($"Enter Transaction {i} (withdrawal): ");
                string transInput = Console.ReadLine() ?? "";

                if (!decimal.TryParse(transInput, out decimal amount))
                {
                    Console.WriteLine("Error: Transaction amount must contain numbers only (no letters allowed).");
                    i--; // Retry this transaction loop iteration
                }
                else
                {
                    // Check specific limits to display descriptive errors
                    if (amount > account.DailyTransactionLimit)
                    {
                        Console.WriteLine($"Transaction failed: Exceeds daily transaction limit (£{account.DailyTransactionLimit:F2}).");
                    }
                    else if ((account.Balance - amount) < -account.OverdraftLimit)
                    {
                        Console.WriteLine($"Transaction failed: Exceeds overdraft limit (£-{account.OverdraftLimit:F2}).");
                    }
                    else if (account.ProcessTransaction(amount))
                    {
                        Console.WriteLine($"Transaction successful. New balance: £{account.Balance:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Transaction failed.");
                    }
                }
            }

            // Final Summary Display
            Console.WriteLine("\n--- Final Account Summary ---\n");
            Console.WriteLine(account.ToString());
            Console.WriteLine($"Final Balance: £{account.Balance:F2}");
        }

        static string GetValidCustomerName()
        {
            string name;
            do
            {
                Console.Write("Customer Name: ");
                name = Console.ReadLine() ?? "";

                if (!PremiumAccount.IsValidCustomerName(name))
                {
                    Console.WriteLine("Error: Name must contain letters and be between 1 - 50 characters.");
                }
            }
            while (!PremiumAccount.IsValidCustomerName(name));

            return name;
        }

        static string GetValidSortCode()
        {
            string code;
            bool isValid = false;
            do
            {
                Console.Write("Sort Code: ");
                code = Console.ReadLine() ?? "";
                string cleanCode = code.Replace("-", "");

                bool hasOnlyDigits = true;
                foreach (char c in cleanCode)
                {
                    if (!char.IsDigit(c))
                    {
                        hasOnlyDigits = false;
                        break;
                    }
                }

                if (!hasOnlyDigits)
                {
                    Console.WriteLine("Error: Sort code must contain digits only (no letters allowed).");
                }
                else if (cleanCode.Length != 6)
                {
                    Console.WriteLine("Error: Sort code must be exactly 6 digits long.");
                }
                else
                {
                    isValid = true;
                }
            }
            while (!isValid);

            return code.Replace("-", "");
        }

        static string GetValidAccountNumber()
        {
            string accNum;
            bool isValid = false;
            do
            {
                Console.Write("Account Number: ");
                accNum = Console.ReadLine() ?? "";

                bool hasOnlyDigits = true;
                foreach (char c in accNum)
                {
                    if (!char.IsDigit(c))
                    {
                        hasOnlyDigits = false;
                        break;
                    }
                }

                if (!hasOnlyDigits)
                {
                    Console.WriteLine("Error: Account number must contain digits only (no letters allowed).");
                }
                else if (accNum.Length != 8)
                {
                    Console.WriteLine("Error: Account number must be exactly 8 digits long.");
                }
                else
                {
                    isValid = true;
                }
            }
            while (!isValid);

            return accNum;
        }

        static decimal GetValidDeposit()
        {
            decimal deposit;
            bool isValid = false;
            do
            {
                Console.Write("Initial Deposit: ");
                string input = Console.ReadLine() ?? "";

                if (!decimal.TryParse(input, out deposit))
                {
                    Console.WriteLine("Error: Initial deposit must contain numbers only (no letters allowed).");
                }
                else if (!PremiumAccount.IsValidDeposit(deposit))
                {
                    Console.WriteLine("Error: Initial deposit must be between £0.01 and £10,000.00.");
                }
                else
                {
                    isValid = true;
                }
            }
            while (!isValid);

            return deposit;
        }
    }
}