using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class Program
    {
        static void Main(string[] args)
        {
            var monopoly = new MonopolyApp("ASD");
            //monopoly.InitDefaultValues();
            monopoly.Init();
        }
    }
}
