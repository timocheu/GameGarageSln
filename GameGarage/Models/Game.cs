using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameGarage.Models;

public class Game
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Game name is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
    public string? Name { get; set; }

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Please provide a release date")]
    public DateTime ReleaseDate { get; set; }

    [Range(0, 999.99, ErrorMessage = "Price must be between 0 and 999.99")]
    [Column(TypeName = "decimal(8, 2)")]
    public double Price { get; set; }

    [Display(Name = "About the Game")]
    [StringLength(2000, ErrorMessage = "Description is too long")]
    public string? AboutTheGame { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    [Required]
    public string? Developers { get; set; }

    public string? Publishers { get; set; }

    public string? Categories { get; set; }

    [Display(Name = "Header Image URL")]
    [Url(ErrorMessage = "Please enter a valid URL")]
    public string? HeaderImage { get; set; }

    public List<string>? Screenshots { get; set; }

    public string? Tags { get; set; }

    // Using RegularExpression to ensure these are "True/False" or "Yes/No" if not using bools
    [RegularExpression("^(True|False)$", ErrorMessage = "Must be 'True' or 'False'")]
    public string? Windows { get; set; }

    [RegularExpression("^(True|False)$", ErrorMessage = "Must be 'True' or 'False'")]
    public string? Mac { get; set; }

    [RegularExpression("^(True|False)$", ErrorMessage = "Must be 'True' or 'False'")]
    public string? Linux { get; set; }

}
