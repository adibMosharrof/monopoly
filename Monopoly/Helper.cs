using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    static class Helper
    {
        public static string TakeStringInput(string prompt)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (input == null)
            {
                return TakeStringInput(prompt);
            }
            return input;
        }
        public static int TakeNumberInput(string inputPrompt, string errorMessage)
        {
            Console.WriteLine(inputPrompt);
            var input = Console.ReadLine();
            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(errorMessage);
                return TakeNumberInput(inputPrompt, errorMessage);
            }
        }
    }
}
