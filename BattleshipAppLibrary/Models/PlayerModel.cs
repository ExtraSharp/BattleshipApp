namespace BattleshipAppLibrary.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public List<SquareModel> ShipLocations { get; set; } = new();
        public List<SquareModel> ShotsTaken { get; set; } = new();

        public void WelcomePlayer()
        {
            Console.Clear();
            Console.WriteLine($"Welcome { Name }!");
        }
    }
}
