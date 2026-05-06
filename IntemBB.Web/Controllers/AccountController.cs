using System.Security.Claims;
using IntemBB.Web.Data;
using IntemBB.Web.Models;
using IntemBB.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace IntemBB.Web.Controllers;

public class AccountController : Controller
{
    private readonly AuthService _authService;
    private readonly SqlServerHelper _sqlHelper;

    public AccountController(AuthService authService, SqlServerHelper sqlHelper)
    {
        _authService = authService;
        _sqlHelper = sqlHelper;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "IntemBB");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Check system status
        var dt = _sqlHelper.ExecuteQuery("Server33",
            "SELECT * FROM [BB].[dbo].[kmt_oper] WHERE role = @role AND [active] = @active",
            new Dictionary<string, object> { { "role", "2" }, { "active", "1" } });

        if (dt.Rows.Count == 0)
        {
            ModelState.AddModelError("", "Chương trình bị quá hạn vui lòng thử lại sau");
            return View(model);
        }

        var (success, name) = _authService.ValidateUser(model.Username, model.Password);
        if (!success)
        {
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, name),
            new("UserId", model.Username)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(600)
            });

        return RedirectToAction("Index", "IntemBB");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [HttpGet]
    public async Task<IActionResult> LogoutGet()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
