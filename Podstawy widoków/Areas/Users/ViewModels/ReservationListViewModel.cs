using System.Collections;

namespace Podstawy_widoków.Areas.Users.ViewModels;

public class ReservationListViewModel
{
    public ICollection<ReservationRecordViewModel> Reservations { get; set; }
}