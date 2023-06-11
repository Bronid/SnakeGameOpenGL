using TetrisGameOpenGL.Display;

namespace TetrisGameOpenGL
{
    public class Program
    {

        static void StartGame()
        {
            Console.WriteLine("Map width: ");
            int width = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Map height: ");
            int height = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Delay(in ms): ");
            int delay = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Mode with 2 players? (Y/N):");
            string choose = Console.ReadLine();
            bool multiplayerMode = false;
            if (choose == "Y" || choose == "y") multiplayerMode = true;
            using (DisplayManager game = new DisplayManager(800, 800, "SnakeOpenGL", width, height, delay, multiplayerMode))
            {
                game.Run();
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Multiplayer Snake 1.0");
            while (true)
            {
                Console.WriteLine("1. Play game");
                Console.WriteLine("2. Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        StartGame(); break;
                    case "2":
                        Environment.Exit(0); break;
                    default:
                        Console.Clear(); Console.WriteLine("Wrong input"); break;
                }
            }
        }
    }
}