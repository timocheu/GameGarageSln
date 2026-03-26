using GameGarage.Models;

namespace GameGarage.Infrastructure;

public interface IAuditService
{
    Task LogAction(string user, string action, string description);
}

public class EFAuditService : IAuditService
{
    private readonly GameGarageDbContext _context;

    public EFAuditService(GameGarageDbContext context)
    {
        _context = context;
    }

    public async Task LogAction(string user, string action, string description)
    {
        var log = new AuditLog
        {
            UserEmail = user ?? "System",
            Action = action,
            Description = description,
            Timestamp = DateTime.Now
        };

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}
