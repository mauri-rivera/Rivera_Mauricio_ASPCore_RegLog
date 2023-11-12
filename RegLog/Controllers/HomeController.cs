using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegLog.Models;

namespace RegLog.Controllers;

public class HomeController : Controller
{
    private readonly MyContext _context;

    public HomeController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("registro")]
    public IActionResult RegistroUsuario(User nuevoUsuario)
    {
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            nuevoUsuario.Password = Hasher.HashPassword(nuevoUsuario, nuevoUsuario.Password);
            _context.Users.Add(nuevoUsuario);
            _context.SaveChanges();
            HttpContext.Session.SetString("FirstName", nuevoUsuario.FirstName);
            HttpContext.Session.SetString("LastName", nuevoUsuario.LastName);
            HttpContext.Session.SetString("Email", nuevoUsuario.Email);
            HttpContext.Session.SetInt32("UsuarioId", nuevoUsuario.UsuarioId);
            return RedirectToAction("Success");
        }
        return View("Index");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View("Index");
    }

    [HttpPost("login")]
    public IActionResult Login(Login login)
    {
        if (ModelState.IsValid)
        {
            User? usuario = _context.Users.FirstOrDefault(us => us.Email == login.EmailLogin);

            if (usuario != null)
            {
                PasswordHasher<Login> Hasher = new PasswordHasher<Login>();
                var result = Hasher.VerifyHashedPassword(login, usuario.Password, login.PasswordLogin);

                if (result != 0)
                {
                    HttpContext.Session.SetString("FirstName", usuario.FirstName);
                    HttpContext.Session.SetString("LastName", usuario.LastName);
                    HttpContext.Session.SetString("Email", usuario.Email);
                    HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);
                    return RedirectToAction("Success");
                }
            }
            ModelState.AddModelError("PasswordLogin", "Credenciales incorrectas");
            return View("Index");
        }
        return View("Index");
    }

    [HttpGet("success")]
    [SessionCheck]
    public IActionResult Success()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
