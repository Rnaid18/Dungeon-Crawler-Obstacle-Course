using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    abstract class Obstacle
    {

        private Coordinate location;

        public Obstacle(Coordinate coordinate)
        {
            location = coordinate;
        }

        public Obstacle(int x, int y)
        {
            location = new Coordinate(x,y);
        }


        protected Coordinate getLocation()
        {
            return location;
        }

        public abstract string printObstacle();


    }
}
