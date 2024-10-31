namespace DungeonEscapeV2
{
    class Program
    {
        //multidimentionelle arrays er ikke for børn her - hold jer fra det :)
        static char[,] labyrint =
            {
            { 'U', 'T', 'B', 'B', 'F' },
            { 'B', 'T', 'T', 'T', 'T' },
            { 'T', 'T', 'T', 'T', 'B' },
            { 'T', 'B', 'T', 'T', 'T' },
            { 'F', 'B', 'T', 'B', 'N' }
        };
        //Hvert rum i en 5x5 og hvad der er i rummene - se Rum metoden (B i retning)

        static int SpillerX = 2, SpillerY = 2;
        //spilleren starter i midten af labyrinten
        static bool nøgle = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Velkommen til Dungeon Escape! Benyt piletasterne for at bevæge dig: OP, NED, HØJRE eller VENSTRE");

            while (true)
            {
                Console.Write("Vælg en retning: ");
                var input = Console.ReadKey(true).Key;

                if (!Retning(input))
                {
                    Console.WriteLine("Benyt piletasterne for at bevæge dig");
                    continue;
                }

                if (Rum()) break;
            }
        }

        static bool Retning(ConsoleKey key)
        {
            int x = SpillerX, y = SpillerY;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    x--;
                    break;
                case ConsoleKey.DownArrow:
                    x++;
                    break;
                case ConsoleKey.RightArrow:
                    y++;
                    break;
                case ConsoleKey.LeftArrow:
                    y--;
                    break;
                default: return false;
            }

            if (Inbound(x, y))
            {
                if (labyrint[x, y] == 'B')
                {
                    //Blindgyde INDEN i labyrinten
                    Console.WriteLine("En blindgyde, vælg en andet retning");
                }
                else
                {
                    SpillerX = x;
                    SpillerY = y;
                }
            }
            else
            {
                //Blindgyde UDEN for labyrinten
                Console.WriteLine("En blindgyde, vælg en andet retning");
            }
            return true;
        }

        static bool Inbound(int x, int y)
        {
            return x >= 0 && x < labyrint.GetLength(0) && y >= 0 && y < labyrint.GetLength(1);
        }

        static bool Rum()
        {
            switch (labyrint[SpillerX, SpillerY])
            {
                case 'F':
                    Console.WriteLine("Du er blevet fældet! Game over");
                    return true;
                case 'N':
                    if (!nøgle)
                    {
                        Console.WriteLine("Du har fundet en nøgle, hvor mon den hører til?");
                        nøgle = true;
                    }
                    break;
                case 'U':
                    if (nøgle)
                    {
                        Console.WriteLine("Tillykke, du har vundet!");
                        Console.ReadKey();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Du har fundet udgangen, men døren er låst");
                    }
                    break;
                case 'T':
                    Console.WriteLine("Et tomt rum");
                    break;
                default:
                    Console.WriteLine("Fejl");
                    break;
            }
            return false;
        }
    }
}