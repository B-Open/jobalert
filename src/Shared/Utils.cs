using System;

namespace Shared
{
    public class Utils
    {

        public static decimal ConvertKToThousand(string number)
        {
            return (decimal.Parse(number.ToLower().Replace("k",""))) * 1000;
        }
    }
}
