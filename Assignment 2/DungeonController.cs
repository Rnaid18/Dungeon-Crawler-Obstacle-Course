//using MiNET.Blocks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Assignment_2
{
    internal class DungeonController
    {
        private int RowNum;
        private int ColumnNum;
        private Dictionary<Coordinate, Obstacle> Grid;


        public DungeonController(int Columnnum, int Rownum)
        {
            this.RowNum = Rownum;
            this.ColumnNum = Columnnum;
            this.Grid = new Dictionary<Coordinate, Obstacle>();
        }


        public void AddGuard(Coordinate guardCoord)
        {
            Grid[(guardCoord)] = new Guard(guardCoord);

        }


        public void AddFence(Coordinate FenceCoordStart, Coordinate FenceCoordEnd)
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
                    Grid[new Coordinate(i, FenceCoordStart.Y)] = new Fence(i, FenceCoordStart.Y);                    
                }
                else // Vertical Fence 
                {
                    Grid[new Coordinate(FenceCoordStart.X, i)] = new Fence(FenceCoordStart.X, i);
                }
            }
        }

        public void AddSensor(Coordinate SensorLocation, double Range)
        {

            Grid[SensorLocation] = new Sensor(SensorLocation);    //create sensor at coordinate (x,y)

            double SensorRangeIndicator = 0;

            Coordinate TopLeftScan = new Coordinate(SensorLocation.X - (int)Math.Ceiling(Range), SensorLocation.Y - (int)Math.Ceiling(Range));
            Coordinate BottomRightScan = new Coordinate(SensorLocation.X + (int)Math.Ceiling(Range), SensorLocation.Y + (int)Math.Ceiling(Range));

            for (int i = TopLeftScan.Y; i <= BottomRightScan.Y; i++)
            {
                for (int j = TopLeftScan.X; j <= BottomRightScan.X; j++)
                {
                    SensorRangeIndicator = Math.Sqrt(((i - SensorLocation.Y) * (i - SensorLocation.Y)) + ((j - SensorLocation.X) * (j - SensorLocation.X)));

                    if (SensorRangeIndicator <= Range)
                    {
                        Grid[new Coordinate(j, i)] = new Sensor(j, i);
                    }
                }
            }
        }


        public void AddCamera(Coordinate CameraLocation, Direction CameraDirection)
        {
            Grid[CameraLocation] = new Camera(CameraLocation);   

            double Range = 1000;
            int Compass = GetCompass(CameraDirection);

            Coordinate TopLeftScan = new Coordinate(CameraLocation.X - (int)Math.Ceiling(Range), CameraLocation.Y - (int)Math.Ceiling(Range));
            Coordinate BottomRightScan = new Coordinate(CameraLocation.X + (int)Math.Ceiling(Range), CameraLocation.Y + (int)Math.Ceiling(Range));

            for (int i = TopLeftScan.Y; i <= BottomRightScan.Y; i++)
            {
                for (int j = TopLeftScan.X; j <= BottomRightScan.X; j++)
                {
                    if (IsInSector(CameraLocation, Range / 2, Compass, j, i))
                    {
                        Grid[new Coordinate(j, i)] = new Camera(j, i);
                    }
                }

            }
        }


        private int GetCompass(Direction CameraDirection)
        {
            switch (CameraDirection)
            {
                case Direction.North:
                    return 270;
                case Direction.South:
                    return 90;
                case Direction.West:
                    return 180;
                default: return 0;
            }
        }

        private bool IsInSector(Coordinate CameraLocation, double radius, int sector, int x, int y) {
            double Let = 180 / Math.PI * Math.Atan2(y - CameraLocation.Y, x - CameraLocation.X);
            return degreesApart(sector, Let) <= 90 / 2;

        }

        private double degreesApart(double startDegree, double endDegree)
        {
            return Math.Min(degreesLeft(startDegree, endDegree), degreesRight(startDegree, endDegree));
        }

        private double degreesLeft(double startDegree, double endDegree)
        {
            return wrap(endDegree - startDegree, 360);
        }

        private double degreesRight(double startDegree, double endDegree)
        {
            return wrap(startDegree - endDegree, 360);
        }

        private double wrap(double value, double modulo)
        {
            return ((value % modulo) + modulo) % modulo;
        }
   

        public void AddShark(Coordinate NoseLocation, Direction SharkDirection)
        {
            Grid[NoseLocation] = new Shark(NoseLocation);
            Coordinate body;
            Coordinate bottom;
            Coordinate leftFin;
            Coordinate rightFin;
            Coordinate leftTail;
            Coordinate rightTail;
            if (SharkDirection == Direction.North)
            {
                body = new Coordinate(NoseLocation.X, NoseLocation.Y + 1);
                leftFin = new Coordinate(NoseLocation.X - 1, NoseLocation.Y + 1);
                rightFin = new Coordinate(NoseLocation.X + 1, NoseLocation.Y + 1);
                bottom = new Coordinate(NoseLocation.X, NoseLocation.Y + 2);
                leftTail = new Coordinate(NoseLocation.X - 1, NoseLocation.Y + 3);
                rightTail = new Coordinate(NoseLocation.X + 1, NoseLocation.Y + 3);
            }
            else if (SharkDirection == Direction.South) 
            {
                body = new Coordinate(NoseLocation.X, NoseLocation.Y - 1);
                leftFin = new Coordinate(NoseLocation.X - 1, NoseLocation.Y - 1);
                rightFin = new Coordinate(NoseLocation.X + 1, NoseLocation.Y - 1);
                bottom = new Coordinate(NoseLocation.X, NoseLocation.Y - 2);
                leftTail = new Coordinate(NoseLocation.X - 1, NoseLocation.Y - 3);
                rightTail = new Coordinate(NoseLocation.X + 1, NoseLocation.Y - 3);
            }
            else if (SharkDirection == Direction.East)
            {
                body = new Coordinate(NoseLocation.X - 1, NoseLocation.Y);
                leftFin = new Coordinate(NoseLocation.X - 1, NoseLocation.Y - 1);
                rightFin = new Coordinate(NoseLocation.X - 1, NoseLocation.Y + 1);
                bottom = new Coordinate(NoseLocation.X - 2, NoseLocation.Y);
                leftTail = new Coordinate(NoseLocation.X - 3, NoseLocation.Y - 1);
                rightTail = new Coordinate(NoseLocation.X - 3, NoseLocation.Y + 1);
            }
            else
            {
                body = new Coordinate(NoseLocation.X + 1, NoseLocation.Y);
                leftFin = new Coordinate(NoseLocation.X + 1, NoseLocation.Y - 1);
                rightFin = new Coordinate(NoseLocation.X + 1, NoseLocation.Y + 1);
                bottom = new Coordinate(NoseLocation.X + 2, NoseLocation.Y);
                leftTail = new Coordinate(NoseLocation.X + 3, NoseLocation.Y - 1);
                rightTail = new Coordinate(NoseLocation.X + 3, NoseLocation.Y + 1);
            }
            Grid[body] = new Shark(body);
            Grid[bottom] = new Shark(bottom);
            Grid[rightFin] = new Shark(rightFin);
            Grid[leftFin] = new Shark(leftFin);
            Grid[rightTail] = new Shark(rightTail);
            Grid[leftTail] = new Shark(leftTail);
        }

        public string? AddSafePath (Coordinate AgentCurrentLocation, Coordinate AgentFinalDestination)
        {
            int maxX = Math.Max(AgentCurrentLocation.X, AgentFinalDestination.X);
            int maxY = Math.Max(AgentCurrentLocation.Y, AgentFinalDestination.Y);
            int gridSize = Math.Max(maxY, maxX);
            int[,] pathGrid = new int[100, 100]; // Replace with your grid and obstacle information

            foreach (var obstacle in Grid)
            {
                var key = obstacle.Key;
                pathGrid[key.X, key.Y] = 1;

            }

            return FindSafePath(pathGrid, AgentCurrentLocation.X, AgentCurrentLocation.Y, AgentFinalDestination.X, AgentFinalDestination.Y);

         }

        static string FindSafePath(int[,] grid, int x, int y, int endX, int endY)
        {
            if (x == endX && y == endY)
                return "";

            if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1) || grid[x, y] == 1)
                return null; // Return null to indicate that this is not a valid path.

            grid[x, y] = 1; // Mark the cell as visited

            List<Tuple<int, int, string>> possibleMoves = new List<Tuple<int, int, string>>();

            if (x < grid.GetLength(0) - 1 && grid[x + 1, y] != 1)
                possibleMoves.Add(Tuple.Create(x + 1, y, "E"));
            if (x > 0 && grid[x - 1, y] != 1) // Left
                possibleMoves.Add(Tuple.Create(x - 1, y, "W"));
            if (y < grid.GetLength(1) - 1 && grid[x, y + 1] != 1) 
                possibleMoves.Add(Tuple.Create(x, y + 1, "S"));
            if (y > 0 && grid[x, y - 1] != 1) // Up
                possibleMoves.Add(Tuple.Create(x, y - 1, "N"));
           

            possibleMoves.Sort((a, b) => CalculateDistance(a.Item1, a.Item2, endX, endY) - CalculateDistance(b.Item1, b.Item2, endX, endY));

            foreach (var move in possibleMoves)
            {
                int nextX = move.Item1;
                int nextY = move.Item2;
                string direction = move.Item3;

                string path = FindSafePath(grid, nextX, nextY, endX, endY);
                if (path != null)
                    return direction + path;
            }

            grid[x, y] = 0; // Unmark the cell if the path is not successful

            return null; // Return null to indicate that no valid path was found from this cell.
        }


        static int CalculateDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }





        public bool IsCellBlocked(Coordinate currentLocation)
        {
           return Grid.ContainsKey(currentLocation);

        }

        public String GetSafeDirections(Coordinate currentLocation)
        {
            String safeDirections = "";
            if (!IsCellBlocked(new Coordinate(currentLocation.X, currentLocation.Y - 1)))
            {
                safeDirections += "N";
            }
            if (!IsCellBlocked(new Coordinate(currentLocation.X, currentLocation.Y + 1)))
            {
                safeDirections += "S";
            }
            if (!IsCellBlocked(new Coordinate(currentLocation.X + 1 , currentLocation.Y)))
            {
                safeDirections += "E";
            }
            if (!IsCellBlocked(new Coordinate(currentLocation.X - 1, currentLocation.Y)))
            {
                safeDirections += "W";
            }
            return safeDirections;
        }

        public void DisplayGrid(Coordinate TopLeftCell, Coordinate BottomRightCell) 
        { 

            for (int i = TopLeftCell.Y; i <= BottomRightCell.Y; i++)
            {
             
                for (int j = TopLeftCell.X; j <= BottomRightCell.X; j++)
                {
                    Coordinate displayCoordinate = new Coordinate(j, i);
                    if (Grid.ContainsKey(displayCoordinate))
                    {
                        Console.Write(Grid[displayCoordinate].printObstacle()) ;
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
