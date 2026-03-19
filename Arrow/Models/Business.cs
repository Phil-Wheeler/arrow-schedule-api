using System;

namespace Arrow.Models;

public class Business
{
    public Business()
    {
        DateCreated = DateTime.UtcNow;
        Currency = "NZD";
        Locations = new List<Location>();
        Customers = new List<Customer>();
    }

    public Guid Id { get; set;}
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public int Status { get; set; }
    public string Currency { get; set; }
    public string Timezone { get; set; }
    public int Subscription { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string Phone { get; set; }

    public List<Location> Locations { get; set; }
    public List<Customer> Customers { get; set; }
}


