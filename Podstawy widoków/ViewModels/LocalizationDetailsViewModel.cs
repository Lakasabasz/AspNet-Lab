using System.Globalization;
using Podstawy_widoków.Models;

namespace Podstawy_widoków.ViewModels;

public class LocalizationDetailsViewModel
{
    public LocalizationDetailsViewModel(Localization l)
    {
        Id = l.Id;
        ImageUrl = l.ImageUrl;
        Name = l.Name;
        OpenHours =
            $"{CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)l.DayOfWeekBegin)} - {CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)l.DayOfWeekEnd)}, " +
            $"{l.HourBegin}:00 - {l.HourEnd}:00";
        Address = $"{l.Street} {l.StreetNumber}, {l.City}";
    }
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string OpenHours { get; set; }
    public Guid Id { get; set; }
}