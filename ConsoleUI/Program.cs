namespace ConsoleUI
{
    internal class Program
    {
        static List<GridModel> grids = new();
        static int activePlayer = 0;

        static List<int> scores = new() { 0, 0 };
        


        static void Main(string[] args)
        {
            PlayerModel player = CreateNewPlayer();
            DisplayShotGrid(player);
            PlayerModel player2 = CreateNewPlayer();

            DisplayShotGrid(player);
            
            PrintAllShips(player);
            
            

            //activePlayer = 0;

            //MarkShip();
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
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (player.ShotsTaken[5 * i + j].Status == Enums.SquareStatus.Empty)
                    {
                        Console.Write("O");
                    }
                    else if (player.ShotsTaken[5 * i + j].Status == Enums.SquareStatus.Hit)
                    {
                        Console.WriteLine("X");
                    }             
                    
                }
                Console.WriteLine();
            }            
        }

        private static void GetShipLocations(PlayerModel player)
        {
            Console.WriteLine("Please place your ships on the map");

            string location = "";
            
            for (int i = 0; i < 5; i++)
            {
                location = GetIndividualShipLocation(player, i);
                GameLogic.PlaceShip(player, location);
            }            
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
                    isValid = GameLogic.CheckValidity(player, input);                    
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
            Console.WriteLine("Please enter name for new player: ");

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


        //static void CheckIfHit(Square square)
        //{
        //    if (true)
        //    {
        //        var value = grids[activePlayer + 1].Squares.FindIndex(item => item.Coordinates.ToString() == $"{square.Column}{square.Row}");

        //        if (grids[activePlayer + 1].Squares[value].HasShip == true)
        //        {
        //            Console.WriteLine("You have a hit!");
        //            Console.ReadLine();
        //        }
        //    }
        //}

        //static void CheckIfWon()
        //{
        //    if (scores[activePlayer] == 5)
        //    {
        //        Console.WriteLine($"Congrats Player { activePlayer + 1 }! You have won the match!");
        //    }
        //}



        //static void PlaceShips()
        //{
        //    for (int i = 0; i < 2; i++)
        //    {
        //        for (int j = 0; j < 5; j++)
        //        {
        //            SquareModel square = EnterShipCoordinates(j);

        //            try
        //            {
        //                PlaceIndividualShip(square.Coordinates);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //                Console.ReadLine();
        //                j -= 1;
        //            }                    
        //        }
        //        activePlayer = 1;
        //    }
        //}

        //static SquareModel EnterShipCoordinates(int shipNumber)
        //{
        //    Console.Clear();
        //    Console.WriteLine($"Player { activePlayer + 1 }, Ship { shipNumber + 1 }");
        //    Console.WriteLine();

        //    Console.Write("Column: ");
        //    string column = Console.ReadLine();
        //    Console.Write("Row: ");
        //    string row = Console.ReadLine();

        //    SquareModel output = new() { Coordinates = new() { Column = (Enums.Column)System.Enum.Parse(typeof(Enums.Column), column), Row = int.Parse(row) } };

        //    return output;
        //}

        //static GridModel InitialiseGrid()
        //{
        //    GridModel output = new();
        //    List<SquareModel> squares = new();

        //    for (int i = 0; i < 5; i++)
        //    {
        //        for (int j = 1; j < 6; j++)
        //        {
        //            SquareModel square = new();

        //            square.Coordinates = new() { Row = j, Column = (Enums.Column)i };

        //            squares.Add(square);
        //        };
        //    }
        //    output.Squares = squares;

        //    return output;
        //}

        //static void PlaceIndividualShip(Square square)
        //{
        //    var value = grids[activePlayer].Squares.FindIndex(item => item.Coordinates.ToString() == $"{square.Column}{square.Row}");

        //    if (grids[activePlayer].Squares[value].HasShip == true)
        //    {
        //        throw new Exception("You have already placed a ship here. Try again!");
        //    }
        //    else
        //    {
        //        grids[activePlayer].Squares[value].HasShip = true;
        //    }           
        //}
    }
}