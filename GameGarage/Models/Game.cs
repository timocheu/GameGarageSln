using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameGarage.Models;

public class Game {
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public double Price { get; set; }

    public string? AboutTheGame { get; set; }

    public string? Notes { get; set; }

    public string? Developers { get; set; }

    public string? Publishers { get; set; }

    public string? Categories { get; set; }

    public string? HeaderImage { get; set; }

    public List<string>? Screenshots { get; set; }

    public List<string>? Tags { get; set; }

    public string? Windows { get; set; }

    public string? Mac { get; set; }

    public string? Linux { get; set; }

}
