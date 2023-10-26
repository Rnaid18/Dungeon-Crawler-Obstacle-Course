using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal class Camera : Obstacle
    {

        public Camera(Coordinate coordinate) : base(coordinate)
        {
        }
       public Camera(int x, int y) : base(x, y)
        {
        }

        public override string printObstacle()
        {
            return "c";
        }

    }
 
}
