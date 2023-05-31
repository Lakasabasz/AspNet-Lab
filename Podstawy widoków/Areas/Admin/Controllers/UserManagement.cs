using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.Areas.Admin.DTOs;
using Podstawy_widoków.Services;

namespace Podstawy_widoków.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserManagement : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDatabaseInMemory _db;
    private readonly ILogger<UserManagement> _logger;

    public UserManagement(ApplicationDatabaseInMemory db, ILogger<UserManagement> logger, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _logger = logger;
        _userManager = userManager;
    }

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
    
    public async Task<IActionResult> UpdateRole(UpdateRoleRequest request)
    {
        _logger.LogWarning("Role update A:{RequestAdmin}, O:{RequestOperator}, U:{RequestUser}", request.Admin, request.Operator, request.User);
        
        var user = _userManager.Users.FirstOrDefault(x => x.Id == request.UserId);
        if (user is null) return RedirectToAction(nameof(Index));
        
        if (!request.User) await _userManager.RemoveFromRoleAsync(user, "User");
        else await _userManager.AddToRoleAsync(user, "User");

        if (!request.Operator) await _userManager.RemoveFromRoleAsync(user, "Operator");
        else await _userManager.AddToRoleAsync(user, "Operator");

        if (!request.Admin) await _userManager.RemoveFromRoleAsync(user, "Admin");
        else await _userManager.AddToRoleAsync(user, "Admin");
        
        return Redirect("/Admin/UserManagement/Index");
    }
    
    public IActionResult Buttons() => View();
}