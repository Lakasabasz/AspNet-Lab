using System.Collections;

namespace Podstawy_widoków.Areas.Admin.ViewModels;

public class ReservationsViewModel
{
    public string UserName { get; set; }
    public int ReservationsCount { get; set; }
    public int NotClaimedCount { get; set; }
    public IEnumerable<ReservationViewModel> Reservations { get; set; }
}