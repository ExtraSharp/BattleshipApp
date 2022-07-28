using BattleshipAppLibrary;
using static BattleshipAppLibrary.Enums;

namespace BattleshipAppLibrary.Models
{
    public class SquareModel
    {
        public string Column { get; set; }
        public int Row { get; set; }

        public SquareStatus Status { get; set; } = SquareStatus.Empty;

        public string Coordinates => $"{Column}{Row}";
        public override string ToString() => $"{Column}{Row}";
    }
}
