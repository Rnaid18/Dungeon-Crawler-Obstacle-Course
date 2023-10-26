//using AStarNavigator;
//using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal class DungeonView
    {

        private DungeonController Grid;


        public DungeonView(DungeonController grid)
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
                        DisplayObstacleMap();
                        break;
                    case "p":
                        FindSafePath();
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




        void DisplayObstacleMap()
        {

            while (true)
            {
                Coordinate coord1 = CoordinateView.PromptForCoordinate("Enter the location of the top-left cell of the map (X,Y):");
                Coordinate coord2 = CoordinateView.PromptForCoordinate("Enter the location of the bottom-right cell of the map (X,Y):");
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
            Coordinate guardCoord = CoordinateView.PromptForCoordinate("Enter the guard's location (X,Y):");
            Grid.AddGuard(guardCoord);
        }

        void AddFence()
        {
            Coordinate FenceCoordStart;
            Coordinate FenceCoordEnd;
            bool ValidFence = false;

            do
            {
                FenceCoordStart = CoordinateView.PromptForCoordinate("Enter the location where the fence starts (X,Y):");
                FenceCoordEnd = CoordinateView.PromptForCoordinate("Enter the location where the fence ends (X,Y):");

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
            Coordinate SensorLocation = CoordinateView.PromptForCoordinate("Enter the sensor's location (X,Y):");

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
            Coordinate CameraLocation = CoordinateView.PromptForCoordinate("Enter the camera's location (X,Y):");
            while (true)
            {
                Console.WriteLine("Enter the direction the camera is facing (n, s, e or w):");

                string? CameraDirection = Console.ReadLine();


                if (CameraDirection != null && (CameraDirection == "n" || CameraDirection == "e" || CameraDirection == "s" || CameraDirection == "w"))
                {

                    Grid.AddCamera(new Coordinate(CameraLocation.X,CameraLocation.Y), CameraDirection);
                    return;

                }
                else
                {
                    Console.WriteLine("Invalid direction.");
                }

            }
           
        }


        void FindSafePath () 
        {
            Coordinate AgentCurrentLocation = CoordinateView.PromptForCoordinate("Enter your current location (X,Y):");
            Coordinate MissionObjective = CoordinateView.PromptForCoordinate("Enter the location of the mission objective (X,Y):");
            while (true)
            {

                if (MissionObjective == AgentCurrentLocation)
                {
                    Console.WriteLine("Agent, you are already at the objective.");
                    break;
                }
                else if (Grid.IsCellBlocked(MissionObjective))
                {
                    Console.WriteLine("The objective is blocked by an obstacle and cannot be reached.");
                    break;
                }
                else
                {
                    String? path = Grid.AddSafePath(AgentCurrentLocation, MissionObjective);
                    if (path == null)
                    {
                        Console.WriteLine("There is no safe path to the objective.");
                    }
                    else {  
                        Console.WriteLine("The following path will take you to the objective:");
                        Console.WriteLine(path);
                     }
                    return;
                }

            }

        }


        void ShowSafeDirections()
        {

            Coordinate currentLocation = CoordinateView.PromptForCoordinate("Enter your current location (X,Y):");

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
