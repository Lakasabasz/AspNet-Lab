using Podstawy_widoków.Models;

namespace Podstawy_widoków.ViewModels;

public class LocalizationShortViewModel
{
    public LocalizationShortViewModel(Localization l)
    {
        Id = l.Id;
        Name = l.Name;
    }
    public Guid Id { get; }
    public string Name { get; }
}