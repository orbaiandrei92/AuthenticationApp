using LoginLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoginLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var userAccount = new UserAccount();
            var token = userAccount.Login("b", "bbbbbb").Result;
            Console.WriteLine(token);
        }
    }
}
