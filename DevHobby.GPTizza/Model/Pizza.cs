using System.ComponentModel.DataAnnotations;

namespace DevHobby.GPTizza.Model;

public class Pizza
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string ShortDescription { get; set; } = default!;

    public string LongDescription { get; set; } = default!;

    [Required]
    public string ImageUrl { get; set; } = default!;

    public string AltText { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public bool IsPizzaOfTheWeek { get; set; }
}
