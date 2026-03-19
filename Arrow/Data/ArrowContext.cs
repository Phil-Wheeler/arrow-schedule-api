
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Arrow.Models;

namespace Arrow.Data;

public class ArrowContext : IdentityDbContext<ApplicationUser>
{
    public ArrowContext(DbContextOptions<ArrowContext> options) : base(options)
    {
        
    }

    public DbSet<TokenInfo> TokenInfos { get; set; }

    public DbSet<Business> Businesses { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Customer> Customers { get; set; }


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Money>()
            .HaveConversion<CurrencyConverter>();
    }
    
}