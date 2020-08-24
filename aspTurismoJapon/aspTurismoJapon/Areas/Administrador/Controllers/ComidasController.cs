using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspTurismoJapon.Models;
using aspTurismoJapon.Models.ViewModels;
using aspTurismoJapon.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace aspTurismoJapon.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class ComidasController : Controller
    {
        public IHostingEnvironment Environment { get; set; }

        public ComidasController(IHostingEnvironment env)
        {
            Environment = env;
        }

        public IActionResult Index()
        {
            ComidasRepository comidasRepository = new ComidasRepository();
            var comidasIEnumerable = comidasRepository.GetComidasConNavigation();
            return View(comidasIEnumerable);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Comidas_ViewModel comidas_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ComidasRepository comidasRepository = new ComidasRepository();
                    var comidaResult = comidasRepository.GetComidaByNombre(comidas_ViewModel.Nombre);

                    if (comidaResult == null)
                    {
                        comidasRepository.InsertComidasViewModel(comidas_ViewModel);

                        if (comidas_ViewModel.PortadaFile == null)
                        {
                            comidasRepository.SetNOPhoto(comidas_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (comidas_ViewModel.PortadaFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG.");
                            ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                            return View(comidas_ViewModel);
                        }
                        else if (comidas_ViewModel.PortadaFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                            return View(comidas_ViewModel);
                        }
                        else
                        {
                            comidasRepository.SetPhoto(comidas_ViewModel.Id, comidas_ViewModel.PortadaFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("Comidas", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe una comida con este nombre");
                        ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                        return View(comidas_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                    return View(comidas_ViewModel);
                }
            }
            else
            {
                ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                return View(comidas_ViewModel);
            }
        }

        public IActionResult Editar(int id)
        {
            ComidasRepository comidasRepository = new ComidasRepository();
            var comidaResult = comidasRepository.GetComidasById(id);
            return View(comidaResult);
        }

        [HttpPost]
        public IActionResult Editar(Comidas_ViewModel comidas_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ComidasRepository comidasRepository = new ComidasRepository();
                    var comidaResult = comidasRepository.GetComidaByNombre(comidas_ViewModel.Nombre);

                    if (comidaResult == null)
                    {
                        comidasRepository.UpdateComidasViewModel(comidas_ViewModel);

                        if (comidas_ViewModel.PortadaFile == null)
                        {
                            //comidasRepository.SetNOPhoto(comidas_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (comidas_ViewModel.PortadaFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG.");
                            ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                            return View(comidas_ViewModel);
                        }
                        else if (comidas_ViewModel.PortadaFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                            return View(comidas_ViewModel);
                        }
                        else
                        {
                            comidasRepository.SetPhoto(comidas_ViewModel.Id, comidas_ViewModel.PortadaFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("Comidas", "Administrador");
                    }
                    else if (comidaResult.Id == comidas_ViewModel.Id)
                    {
                        comidaResult.Nombre = comidas_ViewModel.Nombre;
                        comidaResult.Descripcion = comidas_ViewModel.Descripcion;
                        comidaResult.IdCiudad = comidas_ViewModel.IdCiudad;

                        comidasRepository.Update(comidaResult);

                        if (comidas_ViewModel.PortadaFile == null)
                        {
                            //comidasRepository.SetNOPhoto(comidas_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (comidas_ViewModel.PortadaFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG.");
                            ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                            return View(comidas_ViewModel);
                        }
                        else if (comidas_ViewModel.PortadaFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                            return View(comidas_ViewModel);
                        }
                        else
                        {
                            comidasRepository.SetPhoto(comidas_ViewModel.Id, comidas_ViewModel.PortadaFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("Comidas", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe una comida con este nombre");
                        ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                        return View(comidas_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.IdCiudad = comidas_ViewModel.IdCiudad;
                    return View(comidas_ViewModel);
                }
            }
            else
            {
                return View(comidas_ViewModel);
            }
        }

        public IActionResult Eliminar(int id)
        {
            ComidasRepository comidasRepository = new ComidasRepository();
            var comidaResult = comidasRepository.GetById(id);
            return View(comidaResult);
        }

        [HttpPost]
        public IActionResult Eliminar(Comidas comidas)
        {
            ComidasRepository comidasRepository = new ComidasRepository();
            var comidaResult = comidasRepository.GetById(comidas.Id);

            if (comidaResult == null)
            {
                ModelState.AddModelError("", "Esta comida no existe o ya fue eliminada");
                return View(comidaResult);
            }
            else
            {
                comidasRepository.Delete(comidaResult);
                return RedirectToAction("Comidas", "Administrador");
            }
        }
    }
}