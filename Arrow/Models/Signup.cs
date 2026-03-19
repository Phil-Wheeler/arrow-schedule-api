
using System.ComponentModel.DataAnnotations;

namespace Arrow.Models;

public class Signup
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = String.Empty;

    [Required]
    [MaxLength(30)]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;

    [Required]
    [MaxLength(30)]
    public string Password { get; set; } = String.Empty;
}