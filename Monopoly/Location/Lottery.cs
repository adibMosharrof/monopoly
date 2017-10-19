using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class Lottery : Location
    {
        public Lottery(string[] values)
        {
            Name = values[0];
            Id = Convert.ToInt16(values[1]);
        }
    }
}
