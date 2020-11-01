using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ServerPalindromeLibrary;

namespace IO_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerPalindromeAPM serverAsync = new ServerPalindromeAPM(IPAddress.Parse("127.0.0.1"), 5544);
            try
            {
                serverAsync.Start();
            }
            catch(System.IO.IOException e) { }
        }
    }
}
