namespace GameGarage.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public string Category { get; set; }
    }
}
