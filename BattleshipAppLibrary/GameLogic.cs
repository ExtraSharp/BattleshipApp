using BattleshipAppLibrary.Models;
using System.Data.Common;

namespace BattleshipAppLibrary
{
    public class GameLogic
    {
        static List<GridModel> grids = new();
        static int activePlayer = 0;

        static List<int> scores = new() { 0, 0 };

        static List<PlayerModel> players = new();

        public static void CreatePlayer(string name)
        {
            PlayerModel player = new() { Name = name };

            InitialiseGrid(player);

            players.Add(player);
        }

        public static void InitialiseGrid(PlayerModel player)
        {
            List<string> columns = new() { "A", "B", "C", "D", "E" };
            List<int> rows = new() { 1, 2, 3, 4, 5 };

            foreach (var column in columns)
            {
                foreach (var row in rows)
                {
                    AddSquare(player, column, row);
                }
            }
        }

        private static void AddSquare(PlayerModel player, string column, int row)
        {
            SquareModel square = new() { Column = column, Row = row };
            player.ShipLocations.Add(square);
        }

        //public static bool CheckIfHit(Square square)
        //{
        //    var value = grids[activePlayer + 1].Squares.FindIndex(item => item.Coordinates.ToString() == $"{square.Column}{square.Row}");

        //    if (grids[activePlayer + 1].Squares[value].HasShip == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public static void PlaceShipOnMap(Square square)
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
