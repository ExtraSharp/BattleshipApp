namespace ConsoleUI
{
    internal class Program
    {
        static int player1score = 0;
        static int player2score = 0;

        static void Main(string[] args)
        {
            DisplayWelcomeMessage();
            
            PlayerModel player = CreateNewPlayer();
            PlayerModel player2 = CreateNewPlayer();

            // Mirror Grids
            player.ShotsTaken = player2.ShipLocations;
            player2.ShotsTaken = player.ShipLocations;

            
            DisplayShotGrid(player2);

            string playerInput = "";

            do
            {
                playerInput = GetShotLocation(player);
                Console.Clear();
                if (GameLogic.PlaceShot(player, playerInput) == true)
                {
                    Console.WriteLine("That was a hit!");
                    player1score += 1;
                }
                else
                {
                    Console.WriteLine("Miss! Better luck next time.");
                }
                DisplayShotGrid(player);
            } while (player1score <= 4);
            
            PrintAllShips(player);
            
            

            //activePlayer = 0;

            //MarkShip();
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("============================================");
            Console.WriteLine("==========Welcome to Battleships!===========");
            Console.WriteLine("============================================");
        }

        private static PlayerModel CreateNewPlayer()
        {
            PlayerModel output = new();

            output.Name = GetPlayerName();

            output.WelcomePlayer();

            GameLogic.InitialiseGrid(output);

            GetShipLocations(output);

            return output;
        }

        private static void DisplayShotGrid(PlayerModel player)
        {
            Console.WriteLine("Your shot grid (O = miss, X = hit)");
            Console.WriteLine();
            Console.WriteLine("--A-B-C-D-E");
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"{i + 1}-");
                
                for (int j = 0; j < 5; j++)
                {
                    if (player.ShotsTaken[5 * i + j].Status == Enums.SquareStatus.Miss)
                    {
                        Console.Write("O");
                    }
                    else if (player.ShotsTaken[5 * i + j].Status == Enums.SquareStatus.Hit)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("?");
                    }
                    if (j < 4)
                    {
                        Console.Write("-");
                    }                                
                }
                Console.WriteLine();
            }            
        }

        private static void GetShipLocations(PlayerModel player)
        {
            Console.WriteLine();
            Console.WriteLine("Please place your ships on the map");
            Console.WriteLine();

            string location = "";
            
            for (int i = 0; i < 5; i++)
            {
                location = GetIndividualShipLocation(player, i);
                GameLogic.PlaceShip(player, location);
            }            
        }

        private static string GetShotLocation(PlayerModel player)
        {
            string input = "";
            bool isValid = false;

            do
            {
                Console.WriteLine($" {player.Name}, Please enter a location for your shot");
                input = Console.ReadLine();

                try
                {
                    isValid = GameLogic.CheckValidity(player, input, "shot");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (isValid == false);

            return input;
        }

        private static string GetIndividualShipLocation(PlayerModel player, int i)
        {
            bool isValid = false;
            string input = "";

            do
            {
                Console.Write($"Please enter a location for ship #{ i + 1 } (A1 - E5): ");
                input = Console.ReadLine();

                try
                {
                    isValid = GameLogic.CheckValidity(player, input, "ship");                    
                }
                catch (Exception ex)                
                {
                    Console.WriteLine(ex.Message);
                }

            } while (isValid == false);

            return input;
        }

        public static string GetPlayerName()
        {
            Console.WriteLine();
            Console.Write("Please enter name for new player: ");

            return Console.ReadLine();         
        }

        private static void PrintAllShips(PlayerModel player)
        {
            var ships = player.ShipLocations.FindAll(item => item.Status == Enums.SquareStatus.Ship);

            foreach (var s in ships)
            {
                Console.WriteLine(s.ToString());
            }
        }
    }
}