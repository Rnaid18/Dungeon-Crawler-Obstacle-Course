using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal class Fence : Obstacle
    {
        public Fence(Coordinate coordinate) : base(coordinate)
        {
        }

       public Fence(int x, int y) : base(x, y)
        {
        }


        public override string printObstacle()
        {
            return "f";
        }


    }
}
