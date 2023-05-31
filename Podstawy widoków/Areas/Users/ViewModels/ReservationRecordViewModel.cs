namespace Podstawy_widoków.Areas.Users.ViewModels;

public class ReservationRecordViewModel
{
    public string Image { get; set; }
    public string VehicleName { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public Guid ReservationId { get; set; }
}