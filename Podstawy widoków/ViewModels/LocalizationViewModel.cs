using Podstawy_widoków.Models;

namespace Podstawy_widoków.ViewModels;

public class LocalizationViewModel
{
    public LocalizationViewModel(Localization localization)
    {
        Id = localization.Id;
        ImageUrl = localization.ImageUrl;
        Name = localization.Name;
        bool day = false;
        bool hour = false;
        if (localization.DayOfWeekBegin < localization.DayOfWeekEnd)
            day = localization.DayOfWeekBegin <= (int)DateTime.Now.DayOfWeek &&
                  localization.DayOfWeekEnd >= (int)DateTime.Now.DayOfWeek;
        else if (localization.DayOfWeekEnd < localization.DayOfWeekBegin)
            day = localization.DayOfWeekBegin >= (int)DateTime.Now.DayOfWeek ||
                  localization.DayOfWeekEnd <= (int)DateTime.Now.DayOfWeek;
        else day = true;
        
        if (localization.HourBegin < localization.HourEnd)
            hour = localization.HourBegin <= DateTime.Now.Hour && localization.HourEnd > DateTime.Now.Hour;
        else if (localization.HourEnd < localization.HourBegin)
            hour = localization.HourBegin >= DateTime.Now.Hour || localization.HourEnd < DateTime.Now.Hour;
        else hour = true;

        Opened = day && hour;
        Address = $"{localization.Street} {localization.StreetNumber}, {localization.City}";
    }
    
    public Guid Id { get; }
    public string ImageUrl { get; }
    public string Name { get; }
    public string Address { get; }
    public bool Opened { get; }
}