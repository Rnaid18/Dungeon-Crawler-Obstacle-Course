//using AStarNavigator;
//using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
//using MiNET.Blocks;
//using MiNET.Plugins;
//using MiNET.UI;
//using MiNET.Utils.Vectors;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal class DungeonController
    {

        private DungeonGrid Grid;


        public DungeonController(DungeonGrid grid)
        {
            this.Grid = grid;
        }

        public void display()
        {
            bool ExitFlag = false;

            while (!ExitFlag)
            {
                Console.WriteLine("g) Add 'Guard' obstacle");
                Console.WriteLine("f) Add 'Fence' obstacle");
                Console.WriteLine("s) Add 'Sensor' obstacle");
                Console.WriteLine("c) Add 'Camera' obstacle");
                Console.WriteLine("d) Show safe directions");
                Console.WriteLine("m) Display obstacle map");
                Console.WriteLine("p) Find safe path");
                Console.WriteLine("x) Exit");
                Console.WriteLine("Enter code:");

                string? selection = Console.ReadLine();

                switch (selection)
                {
                    case "g":
                        AddGuard();
                        break;
                    case "f":
                        AddFence();
                        break;
                    case "s":
                        AddSensor();
                        break;
                    case "c":
                        AddCamera();
                        break;
                    case "d":
                        ShowSafeDirections();
                        break;
                    case "m":
                        //execute option mmethod()
                        DisplayObstacleMap();
                        break;
                    case "p":
                        //execute option pmethod()
                        break;
                    case "x":
                        ExitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        Console.WriteLine("Enter code:");
                        break;
                }
            }


        }



        private void Fence(int x, int y)
        {

        }
        private void sensor(int x, int y)
        {

        }




        void DisplayObstacleMap()
        {

            while (true)
            {
                Cell coord1 = Cell.PromptForCoordinate("Enter the location of the top-left cell of the map (X,Y):");
                Cell coord2 = Cell.PromptForCoordinate("Enter the location of the bottom-right cell of the map (X,Y):");
                if (coord2.X > coord1.X && coord2.Y > coord1.Y)
                {
                
                    this.Grid.DisplayGrid(coord1, coord2); //Display the grid
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid map specification.");
                }
            }

        }

        void AddGuard()
        {
            Cell guardCoord = Cell.PromptForCoordinate("Enter the guard's location (X,Y):");
            Grid.AddGuard(guardCoord);
        }

        void AddFence()
        {
            Cell FenceCoordStart;
            Cell FenceCoordEnd;
            bool ValidFence = false;

            do
            {
                FenceCoordStart = Cell.PromptForCoordinate("Enter the location where the fence starts (X,Y):");
                FenceCoordEnd = Cell.PromptForCoordinate("Enter the location where the fence ends (X,Y):");

                ValidFence = (FenceCoordStart.X == FenceCoordEnd.X && FenceCoordStart.Y != FenceCoordEnd.Y) ||
                             (FenceCoordStart.Y == FenceCoordEnd.Y && FenceCoordStart.X != FenceCoordEnd.X);

                if (!ValidFence)
                {
                    Console.WriteLine("Fences must be horizontal or vertical.");
                }
            } while (!ValidFence);
    
            Grid.AddFence(FenceCoordStart, FenceCoordEnd);
        }



        void AddSensor()
        {
            Cell SensorLocation = Cell.PromptForCoordinate("Enter the sensor's location (X,Y):");

            while (true)
            {
                Console.WriteLine("Enter the sensor's range (in klicks):");

                string? SensorRange = Console.ReadLine();


                if (SensorRange != null && float.TryParse(SensorRange, out float value) && value > 0)
                {

                    Grid.AddSensor(SensorLocation, value);
                    return;

                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }

            }


        }

        void AddCamera()
        {
            Grid.AddCamera(new Cell(2,3), "S");
        }








        void ShowSafeDirections()
        {

            Cell currentLocation = Cell.PromptForCoordinate("Enter your current location (X,Y):");

            if (Grid.IsCellBlocked(currentLocation))
            {
                Console.WriteLine("Agent, your location is compromised. Abort mission.");
            }
            else
            {
                String safeDirections = Grid.GetSafeDirections(currentLocation);
                if (safeDirections == "")
                {
                    Console.WriteLine("You cannot safely move in any direction. Abort mission.");
                }
                else
                {
                    Console.WriteLine($"You can safely take any of the following directions: {safeDirections}");
                }
            }

        }
    }
}
