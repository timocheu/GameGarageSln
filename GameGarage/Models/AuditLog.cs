using System.ComponentModel.DataAnnotations;

namespace GameGarage.Models;

public class AuditLog
{
    public int Id { get; set; }

    [Required]
    public string UserEmail { get; set; } = string.Empty;

    [Required]
    public string Action { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.Now;
}
