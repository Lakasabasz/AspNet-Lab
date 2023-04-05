using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using Podstawy_widoków.DTOs;

namespace Podstawy_widoków.Models;

public class Localization
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; }
    public string ImageUrl { get; set; }
    public int DayOfWeekBegin { get; set; }
    public int DayOfWeekEnd { get; set; }
    public int HourBegin { get; set; }
    public int HourEnd { get; set; }
    public string City { get; set; }
    public int StreetNumber { get; set; }
    public string Street { get; set; }

    public static Localization FromAddLocalization(AddLocation l)
    {
        return new Localization()
        {
            Name = l.Name,
            ImageUrl = l.ImageUrl ?? String.Empty,
            DayOfWeekBegin = l.DayBegin,
            DayOfWeekEnd = l.DayEnd,
            HourBegin = l.HourBegin,
            HourEnd = l.HourEnd,
            City = l.City,
            StreetNumber = l.BuildingNumber,
            Street = l.Street
        };
    }
}