using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Cell PromptForCoordinate(String userPrompt)
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
                        return new Cell(x, y);
                    }
         
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
            }
        }
    }
}
