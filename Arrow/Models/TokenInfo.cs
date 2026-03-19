
using System.ComponentModel.DataAnnotations;

public class TokenInfo
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string UserName { get; set; } = String.Empty;

    [Required]
    [MaxLength(200)]
    public string RefreshToken { get; set; } = String.Empty;

    [Required]
    public DateTime ExpiredAt { get; set; }
}