
using System;
using System.Text;

namespace CurrencyConverterLibrary
{
    public class CurrencyConverter
    {
        private static readonly string[] Units = { "", "thousand", "million", "billion" };
        private static readonly string[] Ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static readonly string[] Teens = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static readonly string[] Tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        public static string ConvertToWords(double amount)
        {
            if (amount < 0 || amount > 999999999.99)
            {
                return "Invalid amount";
            }

            long dollars = (long)amount;
            int cents = (int)((amount - dollars) * 100);

            StringBuilder result = new StringBuilder();

            result.Append(ConvertToWordsInternal(dollars, 0));
            result.Append(" dollars");

            if (cents > 0)
            {
                result.Append(" and ");
                result.Append(ConvertToWordsInternal(cents, 0));
                result.Append(" cents");
            }

            return result.ToString();
        }

        private static string ConvertToWordsInternal(long number, int level)
        {
            if (number == 0)
                return "";

            StringBuilder result = new StringBuilder();
            int currentLevel = level;

            while (number > 0)
            {
                int part = (int)(number % 1000);
                number /= 1000;

                if (part > 0)
                {
                    result.Insert(0, ConvertToWordsInternalHelper(part, currentLevel));
                }

                currentLevel++;
            }

            return result.ToString();
        }

        private static string ConvertToWordsInternalHelper(int number, int level)
        {
            StringBuilder result = new StringBuilder();

            int hundreds = number / 100;
            int remainder = number % 100;

            if (hundreds > 0)
            {
                result.Append(Ones[hundreds] + " hundred ");
            }

            if (remainder > 0)
            {
                if (result.Length > 0)
                {
                    result.Append("and ");
                }

                if (remainder < 10)
                {
                    result.Append(Ones[remainder]);
                }
                else if (remainder < 20)
                {
                    result.Append(Teens[remainder - 11]);
                }
                else
                {
                    result.Append(Tens[remainder / 10]);
                    if (remainder % 10 > 0)
                    {
                        result.Append("-" + Ones[remainder % 10]);
                    }
                }
            }

            if (level > 0)
            {
                result.Append(Units[level] + " ");
            }

            return result.ToString();
        }
    }
}
