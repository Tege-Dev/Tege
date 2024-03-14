using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Capitalize the first letter and convert the rest to lowercase
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }

}
