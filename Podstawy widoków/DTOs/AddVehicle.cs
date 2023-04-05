namespace Podstawy_widoków.DTOs;

public class AddVehicle
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Type { get; set; }
    public Guid Localization { get; set; }
}