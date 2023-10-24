//using MiNET.Blocks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Assignment_2
{
    internal class DungeonGrid
    {
        private int RowNum;
        private int ColumnNum;
        private Dictionary<(int, int), Cell> Grid;
 

        public DungeonGrid (int Columnnum, int Rownum)
        {
            this.RowNum = Rownum;
            this.ColumnNum = Columnnum;
            this.Grid = new Dictionary<(int, int), Cell>();
        }

        public void AddGuard(Cell guardCoord)
        {
            Grid[(guardCoord.Y, guardCoord.X)] = new Guard(guardCoord.Y, guardCoord.X);

        }

        public void AddFence(Cell FenceCoordStart, Cell FenceCoordEnd)
        {

            //determine fence orientation
            bool HorizontalFence = FenceCoordStart.Y == FenceCoordEnd.Y; // Horizontal Fence

            int Min = HorizontalFence ? Math.Min(FenceCoordStart.X, FenceCoordEnd.X) : Math.Min(FenceCoordStart.Y, FenceCoordEnd.Y); // returns smallest x & y value between two coordinates
            int Max = HorizontalFence ? Math.Max(FenceCoordStart.X, FenceCoordEnd.X) : Math.Max(FenceCoordStart.Y, FenceCoordEnd.Y); // returns largest x & y value between two coordinates

            //Add Fence along X or Y Axis
            for (int i = Min; i <= Max; i++)
            {

                if (HorizontalFence)
                {
                    Grid[(FenceCoordStart.Y, i)] = new Fence(FenceCoordStart.Y, i);
                } 
                else // Vertical Fence 
                {
                    Grid[(i, FenceCoordStart.X)] = new Fence(i, FenceCoordStart.X);
                }   
            }
        }

        public void AddSensor(Cell SensorLocation, double Range)
        {

            Grid[(SensorLocation.Y, SensorLocation.X)] = new Sensor(SensorLocation.Y, SensorLocation.X);    //create sensor at coordinate (x,y)

            double SensorRangeIndicator = 0;

            Cell TopLeftScan = new Cell(SensorLocation.X - (int)Math.Ceiling(Range), SensorLocation.Y - (int)Math.Ceiling(Range));
            Cell BottomRightScan = new Cell(SensorLocation.X + (int)Math.Ceiling(Range), SensorLocation.Y + (int)Math.Ceiling(Range));

            for (int i = TopLeftScan.Y; i <= BottomRightScan.Y; i++)
            {
                for (int j = TopLeftScan.X; j <= BottomRightScan.X; j++)
                {
                    SensorRangeIndicator = Math.Sqrt(((i - SensorLocation.Y) * (i - SensorLocation.Y)) + ((j - SensorLocation.X) * (j - SensorLocation.X)));

                    if (SensorRangeIndicator <= Range)
                    {
                        Grid[(i, j)] = new Sensor(i, j);
                    }
                }
            }
        }


        

        public void AddCamera(Cell CameraLocation, String Direction)
        {
            for (int i = CameraLocation.X; i < 100; i++)
            {
                 for (int j = 0; j < (i + 1); j++)
                {
                    if (j == 0)
                    {
                        Grid[(CameraLocation.X + i, CameraLocation.Y)] = new Camera(CameraLocation.X + i, CameraLocation.Y);
                     }
                    else
                    {
                        Grid[(CameraLocation.X + i, CameraLocation.Y - j)] = new Camera(CameraLocation.X + i, CameraLocation.Y - j);
                        Grid[(CameraLocation.X + i, CameraLocation.Y + j)] = new Camera(CameraLocation.X + i, CameraLocation.Y + j);
                    }

                }
            }
        }


















        public bool IsCellBlocked(Cell currentLocation)
        {
            return Grid.ContainsKey((currentLocation.Y, currentLocation.X));

        }

        public String GetSafeDirections(Cell currentLocation)
        {
            String safeDirections = "";
            if (!Grid.ContainsKey((currentLocation.Y - 1, currentLocation.X)))
            {
                safeDirections += "N";
            }
            if (!Grid.ContainsKey((currentLocation.Y + 1, currentLocation.X)))
            {
                safeDirections += "S";
            }
            if (!Grid.ContainsKey((currentLocation.Y , currentLocation.X + 1)))
            {
                safeDirections += "E";
            }
            if (!Grid.ContainsKey((currentLocation.Y, currentLocation.X - 1)))
            {
                safeDirections += "W";
            }
            return safeDirections;

        }

        public void DisplayGrid(Cell TopLeftCell, Cell BottomRightCell) 
        { 

            for (int i = TopLeftCell.Y; i <= BottomRightCell.Y; i++)
            {
             
                for (int j = TopLeftCell.X; j <= BottomRightCell.X; j++)
                {
                    if (Grid.ContainsKey((i,j)))
                    {
                        Console.Write(Grid[(i, j)].ToString());
                    }
                    else
                    {
                        Console.Write(".");
                    }       
                }

                Console.WriteLine();
            }
        }




}
}
