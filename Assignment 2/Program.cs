namespace Assignment_2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            DungeonGrid grid = new DungeonGrid(-100, 100); 
            DungeonController dungeon = new DungeonController(grid);
            dungeon.display(); 
           
        }
    }
}