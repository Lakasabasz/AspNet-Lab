namespace Podstawy_widoków.DTOs;

public class AddLocation
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
    public string? ImageUrl { get; set; }
    public int DayBegin { get; set; }
    public int DayEnd { get; set; }
    public int HourBegin { get; set; }
    public int HourEnd { get; set; }
}