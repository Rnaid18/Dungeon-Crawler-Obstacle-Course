//using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal class Guard : Obstacle
    {

        public Guard(Coordinate coordinate) : base(coordinate)
        {

        }
        public Guard(int x, int y) : base(x, y)
        {

        }

        public override string printObstacle()
        {
            return "g";
        }
    }
}


