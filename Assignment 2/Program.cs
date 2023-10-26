namespace Assignment_2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            DungeonController grid = new DungeonController(-1000, 1000); 
            DungeonView dungeon = new DungeonView(grid);
            dungeon.display(); 
           
        }
    }
}