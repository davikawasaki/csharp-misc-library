using System;

namespace CSharpMiscLibrary.Services
{
    /// <summary>
    /// Common service for data manipulation.
    /// </summary>
    public class DataService
    {
        /// <summary>
        /// Check if string has only digits.
        /// </summary>
        /// <param name="str">String to be compared</param>
        /// <returns>Boolean status if string has or not only digits</returns>
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9') return false;
            }

            return true;
        }

        /// <summary>
        /// Check if strings are equal.
        /// </summary>
        /// <param name="a">String 1</param>
        /// <param name="b">String 2</param>
        /// <returns>Boolean status if strings are equal or not.</returns>
        public static bool CheckEqual(string a, string b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Check if number is between two others.
        /// </summary>
        /// <param name="num">Compared number</param>
        /// <param name="a">Lower possible number</param>
        /// <param name="b">Bigger possible number</param>
        /// <returns>Boolean status if number is between A and B.</returns>
        public static bool CheckBetween(double num, double a, double b)
        {
            return num >= a && num <= b;
        }

        /// <summary>
        /// Check if string text is empty or null.
        /// </summary>
        /// <param name="text">String text</param>
        /// <returns>Boolean status if string is empty or null.</returns>
        public static bool IsTextEmptyOrNull(string text)
        {
            return text.Equals("") || text.Equals(null);
        }

        /// <summary>
        /// Get yesterday checking if it's a weekday or not (i.e. monday gets friday)
        /// </summary>
        /// <returns>Yesterday date in string format</returns>
        public static string GetYesterdayWeekDay()
        {
            DateTime today = DateTime.Today;
            string yesterday = today.AddDays(-1).ToString("dd/MM/yyyy");
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                yesterday = today.AddDays(-3).ToString("dd/MM/yyyy");
            }
            return yesterday;
        }
    }
}
