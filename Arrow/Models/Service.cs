using System;

namespace Arrow.Models;

public class Service
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public Money Cost { get; set; }
    public List<Location> Locations { get; set; } = [];

    public List<string> Authorisations { get; set; }

    public override string ToString() {
        return Name;
    }

}

public class AvailableServiceAppointmentSlots
{
    public int Id { get; set; }
    public Service Service { get; set; }
    public DateTime Appointment { get; set; }
    public int DurationInMinutes { get; set; }
}

public class ServiceLocation
{
    public Guid ServiceId { get; set; }
    public int locationId { get; set; }
    public Service Service { get; set; } = null!;
    public Location Location { get; set; } = null!;
}