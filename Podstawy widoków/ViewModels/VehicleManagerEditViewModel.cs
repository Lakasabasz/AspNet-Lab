using Podstawy_widoków.Models;

namespace Podstawy_widoków.ViewModels;

public class VehicleManagerEditViewModel: VehicleManagerAddViewModel
{
    public Guid Id { get; }
    public Guid LocalizationId { get; }
    public string Type { get; }
    public string ImageUrl { get; }
    public string Description { get; }
    public string Name { get; }

    public VehicleManagerEditViewModel(IEnumerable<string> types, IEnumerable<LocalizationShortViewModel> localizations, Vehicle v) : base(types, localizations)
    {
        Id = v.Id;
        LocalizationId = v.CurrentLocalizationId;
        Type = v.TypeId;
        ImageUrl = v.ImageUrl;
        Description = v.Description;
        Name = v.Name;
    }
}