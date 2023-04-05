using Podstawy_widoków.Models;

namespace Podstawy_widoków.ViewModels;

public class RentalIndexViewModel
{
    public RentalIndexViewModel((bool, string)? status, IEnumerable<LocalizationViewModel> localizations)
    {
        Localizations = localizations;
        Status = status;
    }

    public IEnumerable<LocalizationViewModel> Localizations { get; }
    public (bool, string)? Status { get; }
}