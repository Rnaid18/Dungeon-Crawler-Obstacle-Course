﻿//using MiNET.Blocks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Assignment_2
{
    internal class DungeonGrid
    {
        private int RowNum;
        private int ColumnNum;
        private Dictionary<(int, int), Cell> Grid;


        public DungeonGrid(int Columnnum, int Rownum)
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
            Grid[(CameraLocation.Y, CameraLocation.X)] = new Camera(CameraLocation.Y, CameraLocation.X);   

            double Range = 1000;
            int Compass = GetCompass(Direction);

            Cell TopLeftScan = new Cell(CameraLocation.X - (int)Math.Ceiling(Range), CameraLocation.Y - (int)Math.Ceiling(Range));
            Cell BottomRightScan = new Cell(CameraLocation.X + (int)Math.Ceiling(Range), CameraLocation.Y + (int)Math.Ceiling(Range));

            for (int i = TopLeftScan.Y; i <= BottomRightScan.Y; i++)
            {
                for (int j = TopLeftScan.X; j <= BottomRightScan.X; j++)
                {
                    if (IsInSector(CameraLocation, Range / 2, Compass, j, i))
                    {
                        Grid[(i, j)] = new Camera(i, j);
                    }
                }

            }
        }


        private int GetCompass(String Direction)
        {
            switch (Direction)
            {
                case "n":
                    return 270;
                case "s":
                    return 90;
                case "w":
                    return 180;
                default: return 0;
            }
        }

        private bool IsInSector(Cell CameraLocation, double radius, int sector, int x, int y) {
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
   

 

        public string? AddSafePath (Cell AgentCurrentLocation, Cell AgentFinalDestination)
        {
            int maxX = Math.Max(AgentCurrentLocation.X, AgentFinalDestination.X);
            int maxY = Math.Max(AgentCurrentLocation.Y, AgentFinalDestination.Y);
            int gridSize = Math.Max(maxY, maxX);
            int[,] pathGrid = new int[100, 100]; // Replace with your grid and obstacle information

            foreach (var obstacle in Grid)
            {
                var key = obstacle.Key;
                pathGrid[key.Item2, key.Item1] = 1;

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
