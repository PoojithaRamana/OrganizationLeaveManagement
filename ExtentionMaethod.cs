using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtentionMethod
{
    static class ExtensionMethods
    {
        public static DateTime Tomorrow
        {
            get { return DateTime.Now.AddDays(1); }
        }

        public static int Factorial(this int x)

        {
            if (x <= 1) return 1;
            if (x == 2) return 2;
            else
                return x * Factorial(x - 1);
        }
    }
        static void Main(string[] args)
        {
            DateTime tomorrow = ExtensionMethods.Tomorrow;
            int factorial = ExtensionMethods.Factorial(5);
            int x = 7;
            x.Factorial();
            Console.WriteLine(tomorrow);
            Console.WriteLine(factorial);
            Console.WriteLine(x.Factorial());
            Console.ReadLine();
        }  
}
