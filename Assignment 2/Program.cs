namespace Assignment_2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            DungeonGrid grid = new DungeonGrid(-1000, 1000); 
            DungeonController dungeon = new DungeonController(grid);
            dungeon.display(); 
           
        }
    }
}