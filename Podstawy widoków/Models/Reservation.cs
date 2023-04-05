using System.ComponentModel.DataAnnotations.Schema;

namespace Podstawy_widoków.Models;

public class Reservation
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime? ReservationExpire { get; set; }
    public Vehicle Vehicle { get; set; }
}