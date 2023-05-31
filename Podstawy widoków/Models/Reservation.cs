using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Podstawy_widoków.DTOs;
using Podstawy_widoków.Services;

namespace Podstawy_widoków.Models;

public class Reservation
{
    public Reservation(){}

    public Reservation(AddReservation request, IdentityUser claimer, IRepository<Vehicle> vehiclesRepository)
    {
        ReservationDate = request.Begin;
        ReservationExpire = request.End;
        Vehicle = vehiclesRepository.Get(request.VehicleId) ?? throw new NoNullAllowedException("Vehicle not found");
        Claimer = claimer;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ReservationExpire { get; set; }
    public Vehicle Vehicle { get; set; }
    public IdentityUser Claimer { get; set; }
}