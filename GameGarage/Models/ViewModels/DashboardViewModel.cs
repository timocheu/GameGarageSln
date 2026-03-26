using System.Collections.Generic;

namespace GameGarage.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalGames { get; set; }
        public int FreeGamesCount { get; set; }
        public double AveragePrice { get; set; }
        public int CategoryCount { get; set; }
        public List<Game> RecentlyAdded { get; set; } = new();
        public List<Game> TopPriceGames { get; set; } = new();
        public Dictionary<string, int> CategoryStats { get; set; } = new();
    }
}
