using System;

namespace Shared
{
    public class Utils
    {

        public static decimal ConvertKToThousand(string number)
        {
            bool result = number.ToLower().Contains("k");

            if (!result) return decimal.Parse(number);

            return (decimal.Parse(number.ToLower().Replace("k",""))) * 1000;
        }
    }
}
