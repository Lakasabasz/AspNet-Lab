using Podstawy_widoków.ViewModels;

namespace Podstawy_widoków.Models;

public class VehicleManagerAddViewModel
{
    public VehicleManagerAddViewModel(IEnumerable<string> types, IEnumerable<LocalizationShortViewModel> localizations)
    {
        Types = types;
        Localizations = localizations;
    }

    public IEnumerable<string> Types { get; set; }
    public IEnumerable<LocalizationShortViewModel> Localizations { get; set; }
}