using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspTurismoJapon.Models;
using aspTurismoJapon.Models.ViewModels;
using aspTurismoJapon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aspTurismoJapon.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            UsuariosRepository usuariosRepository = new UsuariosRepository();
            var usuariosIEnumerable = usuariosRepository.GetAll();
            return View(usuariosIEnumerable);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Usuarios_ViewModel usuarios_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuariosRepository usuariosRepository = new UsuariosRepository();
                    var usuarioResult = usuariosRepository.GetUsuariosByNombre(usuarios_ViewModel.Nombre);

                    if (usuarioResult == null)
                    {
                        usuariosRepository.InsertUsuariosViewModel(usuarios_ViewModel);
                        return RedirectToAction("Usuarios", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe un usuario con este nombre");
                        return View(usuarios_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(usuarios_ViewModel);
                }
            }
            else
            {
                return View(usuarios_ViewModel);
            }
        }

        public IActionResult Editar(int id)
        {
            UsuariosRepository usuariosRepository = new UsuariosRepository();
            var usuarioResult = usuariosRepository.GetUsuariosById(id);
            return View(usuarioResult);
        }

        [HttpPost]
        public IActionResult Editar(Usuarios_ViewModel usuarios_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuariosRepository usuariosRepository = new UsuariosRepository();
                    var usuarioResult = usuariosRepository.GetUsuariosByNombre(usuarios_ViewModel.Nombre);

                    if (usuarioResult == null)
                    {
                        usuariosRepository.UpdateUsuariosViewModel(usuarios_ViewModel);
                        return RedirectToAction("Usuarios", "Administrador");
                    }
                    else if (usuarioResult.Id == usuarios_ViewModel.Id)
                    {
                        usuarioResult.Nombre = usuarios_ViewModel.Nombre;
                        usuarioResult.Contrasena = usuarios_ViewModel.Contrasena;
                        usuarioResult.Rol = usuarios_ViewModel.Rol;

                        usuariosRepository.Update(usuarioResult);
                        return RedirectToAction("Usuarios", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe un usuario con este nombre");
                        return View(usuarios_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(usuarios_ViewModel);
                }
            }
            else
            {
                return View(usuarios_ViewModel);
            }
        }

        public IActionResult Eliminar(int id)
        {
            UsuariosRepository usuariosRepository = new UsuariosRepository();
            var usuarioResult = usuariosRepository.GetById(id);
            return View(usuarioResult);
        }

        [HttpPost]
        public IActionResult Eliminar(Usuarios usuarios)
        {
            UsuariosRepository usuariosRepository = new UsuariosRepository();
            var usuarioResult = usuariosRepository.GetById(usuarios.Id);

            if (usuarioResult == null)
            {
                ModelState.AddModelError("", "El usuario no existe o ya fue eliminado");
                return View(usuarioResult);
            }
            else
            {
                usuariosRepository.Delete(usuarioResult);
                return RedirectToAction("Usuarios", "Administrador");
            }
        }
    }
}