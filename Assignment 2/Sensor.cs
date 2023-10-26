using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal class Sensor : Obstacle
    {
        public Sensor(Coordinate coordinate) : base(coordinate)
        {

        }
       public Sensor(int x, int y) : base(x, y)
        {

        }

        public override string printObstacle()
        {
            return "s";
        }
    }
}
