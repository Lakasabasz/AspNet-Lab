using Podstawy_widoków.Models;

namespace Podstawy_widoków.ViewModels;

public class VehicleRecordViewModel
{
    public VehicleRecordViewModel(Vehicle v)
    {
        Id = v.Id;
        ImageUrl = v.ImageUrl;
        Name = v.Name;
        Localization = v.CurrentLocalization.Name;
        if (v.Occupied) State = VehicleState.Occupied;
        else if (v.Reservations.Any(x => x.ReservationExpire > DateTime.Now))
            State = VehicleState.Reserved;
        else
            State = VehicleState.Free;
    }
    
    public Guid Id { get; init; }
    public string ImageUrl { get; init; }
    public string Name { get; init; }
    public VehicleState State { get; init; }
    public string Localization { get; set; }
}