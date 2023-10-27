namespace Assignment_2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            DungeonController grid = new DungeonController(); 
            DungeonView dungeon = new DungeonView(grid);
            dungeon.Display(); 
           
        }
    }
}