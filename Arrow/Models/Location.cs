using System;
using System.ComponentModel;


namespace Arrow.Models;

public class Location
{
    public Location()
    {
        BusinessHours = new List<NormalBusinessHours>();
        BusinessHoursExceptions = new List<BusinessHoursExceptions>();
    }

    
    public int Id { get; set; }

    public string Name { get; set; }

    [DisplayName("Address")]
    public string StreetAddress { get; set; }
    public string City { get; set; }

    public override string ToString() {
        return Name;
    }

    public List<Service> Services { get; } = [];
    public List<NormalBusinessHours>? BusinessHours { get; set; }
    public List<BusinessHoursExceptions>? BusinessHoursExceptions { get; set; }
}


public class NormalBusinessHours
{
    public Guid Id { get; set;}
    public string Weekday { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
}

public class BusinessHoursExceptions
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
}