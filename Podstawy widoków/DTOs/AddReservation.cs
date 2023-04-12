namespace Podstawy_widoków.DTOs;

public class AddReservation
{
    public Guid Id { get; set; }
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
}