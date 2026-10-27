using System;

namespace BankApp
{
    public class PremiumAccount
    {
        // Properties
        public string CustomerName { get; set; }
        public string SortCode { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal OverdraftLimit { get; set; }
        public decimal DailyTransactionLimit { get; set; }

        // Constructor
        public PremiumAccount(string customerName, string sortCode, string accountNumber)
        {
            CustomerName = customerName;
            SortCode = sortCode;
            AccountNumber = accountNumber;
            Balance = 0.00m;
            OverdraftLimit = 1000.00m;
            DailyTransactionLimit = 500.00m;
        }

        // ToString override to format account info nicely
        public override string ToString()
        {
            string formattedSortCode = SortCode.Length == 6
                ? $"{SortCode.Substring(0, 2)}-{SortCode.Substring(2, 2)}-{SortCode.Substring(4, 2)}"
                : SortCode;

            return $"{CustomerName} (Sort Code: {formattedSortCode}, Account No: {AccountNumber})";
        }

        // Process Transaction method with conditional checks
        public bool ProcessTransaction(decimal amount)
        {
            if (amount > DailyTransactionLimit)
            {
                return false;
            }

            decimal resultingBalance = Balance - amount;
            if (resultingBalance < -OverdraftLimit)
            {
                return false;
            }

            Balance = resultingBalance;
            return true;
        }

        // Validation: Sort Code
        public static bool IsValidSortCode(string sortCode)
        {
            string cleanCode = sortCode?.Replace("-", "") ?? "";
            if (cleanCode.Length != 6) return false;

            foreach (char c in cleanCode)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }

        // Validation: Account Number
        public static bool IsValidAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber) || accountNumber.Length != 8) return false;

            foreach (char c in accountNumber)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }

        // Validation: Customer Name
        public static bool IsValidCustomerName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            bool hasLetter = false;
            foreach (char c in name)
            {
                if (char.IsLetter(c))
                {
                    hasLetter = true;
                    break;
                }
            }
            return hasLetter && name.Length >= 1 && name.Length <= 50;
        }

        // Validation: Initial Deposit range
        public static bool IsValidDeposit(decimal deposit)
        {
            return deposit >= 0.01m && deposit <= 10000.00m;
        }
    }
}