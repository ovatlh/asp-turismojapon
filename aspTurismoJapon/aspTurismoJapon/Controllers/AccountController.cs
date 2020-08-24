using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using aspTurismoJapon.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace aspTurismoJapon.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string user, string password)
        {
            try
            {
                dbturismojaponContext dbturismojaponContext = new dbturismojaponContext();

                var usuario = dbturismojaponContext.Usuarios.FirstOrDefault(x => x.Nombre == user && x.Contrasena == password);

                if (usuario == null)
                {
                    ModelState.AddModelError("", "El usuario y/o contraseña son incorrectos");
                    return View();
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,usuario.Nombre),
                        new Claim(ClaimTypes.Role,usuario.Rol),
                        new Claim("Id",usuario.Id.ToString())
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);

                    return RedirectToAction("Index", "Administrador/Home");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public IActionResult Denied()
        {
            return View();
        }

        //Acccion para CerrarSesion
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Home");
        }
    }
}