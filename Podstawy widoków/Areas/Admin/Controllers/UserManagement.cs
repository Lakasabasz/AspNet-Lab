using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.Services;

namespace Podstawy_widoków.Areas.Admin.Controllers;

[Area("Admin")]
public class UserManagement : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDatabaseInMemory _db;

    public UserManagement(ApplicationDatabaseInMemory db)
    {
        _db = db;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        var users = _db.Users.Select(x => new {x.Id, x.UserName}).ToList();
        var roles = _db.Roles.Select(x => new { x.Id, x.Name }).ToList();
        var claims = _db.UserRoles.ToList();
        var model = new
        {
            UserName = User.Identity?.Name,
            Users = users.Select(u => new
            {
                UserName = u.UserName,
                Id = u.Id,
                Roles = claims.Where(c => c.UserId == u.Id)
                    .Select(c => 
                        roles.Where(r => r.Id == c.RoleId).Select(r => r.Name).FirstOrDefault())
                    .ToList(),
            }),
            Accounts = _db.Users.Count(),
            ActivatedPercent = (_db.Users.Count(x => x.EmailConfirmed) * 100.0) / _db.Users.Count(),
            InActive = _db.Users.Count(x => !x.EmailConfirmed)
        };
        return View(model);
    }
    
    public IActionResult Buttons() => View();
}