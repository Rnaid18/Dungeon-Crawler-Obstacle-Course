using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal class Shark : Obstacle
    {
        public Shark(Coordinate coordinate) : base(coordinate)
        {

        }
        public Shark(int x, int y) : base(x, y)
        {

        }

        public override string printObstacle()
        {
            return "#";
        }

    }
}
