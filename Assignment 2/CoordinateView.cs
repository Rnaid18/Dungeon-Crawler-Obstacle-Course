using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    internal static class CoordinateView
    {
        public static Coordinate PromptForCoordinate(String userPrompt)
        {
            while (true)
            {
                Console.WriteLine(userPrompt);
                string? Coordinates = Console.ReadLine();
                if (Coordinates != null)
                {
                    string[]? SplitXAndYTopLeft = Coordinates.Split(',');

                    if (SplitXAndYTopLeft.Length == 2 && int.TryParse(SplitXAndYTopLeft[0].Trim(), out int x) && int.TryParse(SplitXAndYTopLeft[1].Trim(), out int y))
                    {
                        return new Coordinate(x, y);
                    }

                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
            }
        }

        public static Direction PromptForDirection(String userPrompt)
        {
            while (true)
            {
                Console.WriteLine(userPrompt);

                string? DirectionString = Console.ReadLine();

                if (DirectionString != null && DirectionString.Length == 1 && Enum.IsDefined(typeof(Direction), (int)DirectionString[0]))
                {
                    return (Direction)DirectionString[0];
                }
                else
                {
                    Console.WriteLine("Invalid direction.");
                }

            }
        }
    }
}
