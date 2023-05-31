using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.Areas.Users.ViewModels;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;

namespace Podstawy_widoków.Areas.Users.Controllers;

[Area("Users"), Authorize(Roles = "User")]
public class ReservationListController : Controller
{
    private readonly IRepository<Reservation> _reservations;
    private readonly UserManager<IdentityUser> _userManager;

    public ReservationListController(IRepository<Reservation> reservations, UserManager<IdentityUser> userManager)
    {
        _reservations = reservations;
        _userManager = userManager;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        return View(new ReservationListViewModel()
        {
            Reservations = _reservations.GetAll()
                .Where(x => x.Claimer == user)
                .Include(x => x.Vehicle)
                .Select(x => new ReservationRecordViewModel()
                {
                    DateEnd = DateOnly.FromDateTime(x.ReservationExpire),
                    DateStart = DateOnly.FromDateTime(x.ReservationDate),
                    Image = x.Vehicle.ImageUrl,
                    ReservationId = x.Id,
                    VehicleName = x.Vehicle.Name
                })
                .ToList()
        });
    }
    
    public IActionResult Cancel(Guid id)
    {
        _reservations.Remove(_reservations.Get(id));
        return RedirectToAction(nameof(Index));
    }
}