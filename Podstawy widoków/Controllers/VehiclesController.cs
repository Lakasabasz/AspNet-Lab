using System.Security.Principal;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.Areas.Users.Controllers;
using Podstawy_widoków.DTOs;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;
using Podstawy_widoków.ViewModels;

namespace Podstawy_widoków.Controllers;

public class VehiclesController : Controller
{
    private readonly IRepository<Vehicle> _vehicles;
    private readonly IRepository<Reservation> _reservations;
    private readonly IMapper _mapper;
    private readonly IValidator<AddReservation> _reservationValidator;
    private readonly UserManager<IdentityUser> _userManager;

    public VehiclesController(IRepository<Vehicle> vehicles, IMapper mapper, IValidator<AddReservation> reservationValidator, IRepository<Reservation> reservations, UserManager<IdentityUser> userManager)
    {
        _vehicles = vehicles;
        _mapper = mapper;
        _reservationValidator = reservationValidator;
        _reservations = reservations;
        _userManager = userManager;
    }

    public IActionResult List()
    {
        return View(new VehicleItemViewModel(
            _vehicles.GetAll()
            .Include(x => x.CurrentLocalization)
            .Include(x => x.Reservations)
            .Select(x => new VehicleRecordViewModel(x)).ToList()));
    }

    public IActionResult Details(Guid id)
    {
        var vm = _vehicles.GetAll()
            .Where(x=>x.Id == id)
            .Include(x => x.Reservations)
            .Include(x => x.CurrentLocalization)
            .First();
        return View(_mapper.Map<VehicleDetailsViewModel>(vm));
    }

    public IActionResult Reserve(Guid id)
    {
        return View(new
        {
            VehicleId = id
        });
    }

    public async Task<IActionResult> ReserveItem(AddReservation reservation)
    {
        if (!(await _reservationValidator.ValidateAsync(reservation)).IsValid) return BadRequest();
        var model = new Reservation(reservation,
            await _userManager.GetUserAsync(User) ?? throw new Exception("No user identity map found"), _vehicles);
        _reservations.Add(model);
        _reservations.SaveChanges();
        return RedirectToAction(nameof(Details), new { Id = reservation.VehicleId });
    }
}