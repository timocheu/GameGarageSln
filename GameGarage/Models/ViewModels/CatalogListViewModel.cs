using GameGarage.Models.ViewModels;

namespace GameGarage.Models.ViewModels
{
    public class CatalogListViewModel
    {
        public IEnumerable<Game> Games { get; set; }
            = [];

        public PagingInfo PagingInfo { get; set; } = new();
    }
}
