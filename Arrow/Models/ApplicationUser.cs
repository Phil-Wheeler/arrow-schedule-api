
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Arrow.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = String.Empty;

}