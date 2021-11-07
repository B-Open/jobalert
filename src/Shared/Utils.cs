
namespace Shared
{
    public class Utils
    {
        /// <summary>
        /// Parse a number in string format to decimal
        /// </summary>
        public static decimal ParseNumber(string number)
        {
            if (number.ToLower().Contains("k"))
            {
                return (decimal.Parse(number.ToLower().Replace("k", ""))) * 1000;
            }
            return decimal.Parse(number);
        }
    }
}
