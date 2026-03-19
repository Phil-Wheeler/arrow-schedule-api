
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Antiforgery;

namespace Arrow.Models;

public class Token
{
    [Required]
    public string AccessToken { get; set; } = String.Empty;

    [Required]
    public string RefreshToken { get; set; } = String.Empty;
}