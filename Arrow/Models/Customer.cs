using System;

namespace Arrow.Models;

public class Customer
{
    public Guid Id { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    

    //public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}