namespace GameGarage.Models
{
    public class EFGarageRepository : IGarageRepository
    {
        private GameGarageDbContext context;
        public EFGarageRepository(GameGarageDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Game> Games => context.Games;
    }
}
