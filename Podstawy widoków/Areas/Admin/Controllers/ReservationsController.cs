using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.Areas.Admin.ViewModels;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;

namespace Podstawy_widoków.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Operator")]
public class ReservationsController : Controller
{
    private readonly IRepository<Reservation> _reservationService;

    public ReservationsController(IRepository<Reservation> reservationService)
    {
        _reservationService = reservationService;
    }

    // GET
    public IActionResult Index()
    {
        return View(new ReservationsViewModel()
        {
            UserName = User.Identity?.Name,
            ReservationsCount = _reservationService.GetAll().Count(),
            NotClaimedCount = _reservationService
                .GetAll()
                .Include(x => x.Vehicle)
                .Include(x => x.Claimer)
                .Where(x => x.Vehicle.Occupied)
                .Count(x => x.ReservationDate < DateTime.Now),
            Reservations = _reservationService.GetAll().Select(x => new ReservationViewModel()
            {
                UserName = x.Claimer.UserName,
                EndDate = x.ReservationExpire.ToString(),
                Id = x.Id.ToString(),
                StartDate = x.ReservationDate.ToString(),
                Vehicle = x.Vehicle.Name
            })
        });
    }
}