using Podstawy_widoków.Models;

namespace Podstawy_widoków.ViewModels;

public class VehicleDetailsViewModel
{
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Id { get; set; }
    public VehicleState State { get; set; }
    public string Localization { get; set; }

    public VehicleDetailsViewModel(Vehicle vehicle)
    {
        ImageUrl = vehicle.ImageUrl;
        Name = vehicle.Name;
        Description = vehicle.Description;
        Id = vehicle.Id;
        Localization = vehicle.CurrentLocalization.Name;
        if (vehicle.Occupied) State = VehicleState.Occupied;
        else if (vehicle.Reservations.Any(x => x.ReservationExpire == null || x.ReservationExpire.Value > DateTime.Now))
            State = VehicleState.Reserved;
        else
            State = VehicleState.Free;
    }

    public VehicleDetailsViewModel()
    {
        ImageUrl = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
        Localization = string.Empty;
    }
}