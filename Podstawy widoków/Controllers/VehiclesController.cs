using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public VehiclesController(IRepository<Vehicle> vehicles, IMapper mapper, IValidator<AddReservation> reservationValidator, IRepository<Reservation> reservations)
    {
        _vehicles = vehicles;
        _mapper = mapper;
        _reservationValidator = reservationValidator;
        _reservations = reservations;
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
        return View();
    }

    public IActionResult ReserveItem(AddReservation reservation)
    {
        if (!_reservationValidator.Validate(reservation).IsValid) return BadRequest();
        _reservations.Add(_mapper.Map<Reservation>(reservation));
        _reservations.SaveChanges();
        return RedirectToAction(nameof(Details), new { Id = reservation.Id });
    }
}