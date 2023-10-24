using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal class Fence : Cell
    {
        public Fence(int x, int y) : base(x, y)
        {
        }


        public override string ToString()
        {
            return "f";
        }


    }
}
