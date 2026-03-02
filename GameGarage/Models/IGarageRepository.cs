namespace GameGarage.Models
{
    public interface IGarageRepository
    {
        IQueryable<Game> Games { get; }
    }
}
