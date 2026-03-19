using System;


namespace Arrow.Models;

public class Booking
{
    public Guid Id { get; set; }
    public DateTime Appointment { get; set; }
    public Customer Customer { get; set; }
    public List<Service> Services { get; set; }
    public Location Location { get; set; }
    public List<BookingApproval> Approvals { get; set; }
    public string Notes { get; set; }
}


public class BookingApproval
{
    public Guid Id { get; set; }

    public string Type { get; set; }
    public int CappedAmount { get; set; }
    public string Notes { get; set; }
}

public class BookingApiModel
{
    public Guid Id { get; set; }
    public DateTime Appointment { get; set; }
    public Guid CustomerId { get; set; }
    public List<Guid> ServiceIds { get; set; }
    public int LocationId { get; set; }
    
}