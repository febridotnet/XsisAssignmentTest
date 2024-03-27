using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Data.Models;

[Table("MovieApp")]
public class Movie
{
    public int Id { get; set; }
    [Required]
    public string? title { get; set; }
    [Required]
    public string? description { get; set; }
    public float? rating { get; set; }
    public DateTime? crated_at { get; set; } = default(DateTime?);
    public DateTime? updated_at { get; set; } = default(DateTime?);
}

