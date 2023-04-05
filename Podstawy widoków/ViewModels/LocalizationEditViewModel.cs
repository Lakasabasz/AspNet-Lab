using Podstawy_widoków.Models;

namespace Podstawy_widoków.ViewModels;

public class LocalizationEditViewModel
{
    public LocalizationEditViewModel(Localization l)
    {
        Id = l.Id;
        Name = l.Name;
        City = l.City;
        StreetNumber = l.StreetNumber;
        Street = l.Street;
        ImageUrl = l.ImageUrl;
        DayBegin = l.DayOfWeekBegin;
        DayEnd = l.DayOfWeekEnd;
        HourEnd = l.HourEnd;
        HourBegin = l.HourBegin;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public string? ImageUrl { get; set; }
    public int DayBegin { get; set; }
    public int DayEnd { get; set; }
    public int HourBegin { get; set; }
    public int HourEnd { get; set; }
}