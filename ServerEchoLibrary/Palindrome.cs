using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPalindromeLibrary
{
    public class Palindrome
    {
        /// <summary>
        /// Determines whether the string is a palindrome.
        /// </summary>
        public static bool IsPalindrome(string value)
        {
            int min = 0;
            int max = value.Length - 1;
            while (true)
            {
                if (min > max)
                {
                    return true;
                }
                char a = value[min];
                char b = value[max];
                if (char.ToLower(a) != char.ToLower(b))
                {
                    return false;
                }
                min++;
                max--;
            }
        }


        /*

foreach (string value in array)
{
   Console.WriteLine("{0} = {1}", value, IsPalindrome(value));
}


"civic",
"deified",
"deleveled",
"devoved",
"dewed",
"Hannah",
"kayak",
"level",
"madam",
"racecar",
"radar",
"redder",
"refer",
"repaper",
"reviver",
"rotator",
"rotor",
"sagas",
"solos",
"sexes",
"stats",
"tenet",

"Dot",
"Net",
"Perls",
"Is",
"Not",
"A",
"Palindrome",
""
*/





    }
}
