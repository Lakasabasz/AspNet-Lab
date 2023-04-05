using Microsoft.AspNetCore.Mvc;
using Podstawy_widoków.DTOs;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;
using Podstawy_widoków.ViewModels;

namespace Podstawy_widoków.Controllers;

public class RentalManagerController : Controller
{
    private readonly IRepository<Localization> _localizations;
    
    public RentalManagerController(IRepository<Localization> localizations)
    {
        _localizations = localizations;
    }

    // GET
    public IActionResult Index(bool? status = null, string description = "")
    {
        return View(new RentalIndexViewModel(status is not null ? (status.Value, description) : null,
            _localizations.GetAll()
                .Select(x => new LocalizationViewModel(x))
                .ToList()));
    }
    
    public IActionResult Add()
    {
        return View();
    }

    public IActionResult AddItem(AddLocation location)
    {
        _localizations.Add(Localization.FromAddLocalization(location));
        _localizations.SaveChanges();
        return RedirectToAction(nameof(Index), new { Status = true, Description = "Dodano pomyślnie" });
    }

    public IActionResult Details(Guid id)
    {
        var localization = _localizations.Get(id);
        if(localization is null) return RedirectToAction(nameof(Index), new { Status = false, Description = "Nieznane ID" });
        return View(new LocalizationDetailsViewModel(localization));
    }

    public IActionResult EditItem(EditLocation location)
    {
        var self = _localizations.Get(location.Id);
        if(self is null) return RedirectToAction(nameof(Details), new { Id = location.Id });
        self.Name = location.Name;
        self.City = location.City;
        self.Street = location.Street;
        self.StreetNumber = location.BuildingNumber;
        self.ImageUrl = location.ImageUrl ?? String.Empty;
        self.HourBegin = location.HourBegin;
        self.HourEnd = location.HourEnd;
        self.DayOfWeekBegin = location.DayBegin;
        self.DayOfWeekEnd = location.DayEnd;
        _localizations.SaveChanges();
        return RedirectToAction(nameof(Details), new { Id = location.Id });
    }

    public IActionResult Edit(Guid id)
    {
        var localization = _localizations.Get(id);
        if(localization is null) return RedirectToAction(nameof(Details), new { Id = id });
        return View(new LocalizationEditViewModel(localization));
    }

    public IActionResult Delete(Guid id)
    {
        _localizations.Remove(_localizations.Get(id));
        _localizations.SaveChanges();
        return RedirectToAction(nameof(Index), new { Status = true, Description = "Usunięto pomyślnie" });
    }
}