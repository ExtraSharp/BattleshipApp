using BattleshipAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Numerics;

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

        public static void PlaceShip(PlayerModel player, string location)
        {
            var value = player.ShipLocations.FindIndex(item => item.ToString() == location);

            player.ShipLocations[value].Status = Enums.SquareStatus.Ship;  
        }

        public static bool PlaceShot(PlayerModel player, string location)
        {
            var index = player.ShotsTaken.FindIndex(item => item.ToString() == location);

            return CheckIfHit(player, index);
        }

        private static bool CheckIfHit(PlayerModel player, int index)
        {
            if (player.ShotsTaken[index].Status == Enums.SquareStatus.Ship)
            {
                player.ShotsTaken[index].Status = Enums.SquareStatus.Hit;
                return true;
            }
            else
            {
                player.ShotsTaken[index].Status = Enums.SquareStatus.Miss;
                return false;
            }
        }

        public static bool CheckValidity(PlayerModel player, string location, string type)
        {
            bool output = false;
            
            int index = player.ShipLocations.FindIndex(f => f.Coordinates == location);

            if (index >= 0)
            {
                if (type == "ship")
                {
                    output = CheckValidatityOfPlacement(player, index);
                }
                else if (type == "shot")
                {
                    output = CheckValidatityOfShot(player, index);
                }             
            }
            else
            {
                throw new Exception("Not a valid location. Please try again.");
            }

            return output;
        }

        private static bool CheckValidatityOfPlacement(PlayerModel player, int index)
        {
            if (player.ShipLocations[index].Status == Enums.SquareStatus.Empty)
            {
                return true;
            }
            else
            {
                throw new Exception("You have already placed a ship here. Try another location.");
            }
        }

        private static bool CheckValidatityOfShot(PlayerModel player, int index)
        {
            if (player.ShotsTaken[index].Status == Enums.SquareStatus.Empty || player.ShotsTaken[index].Status == Enums.SquareStatus.Ship)
            {
                return true;
            }
            else
            {
                throw new Exception("You have already fired at this spot. Please try a different location.");
            }
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
