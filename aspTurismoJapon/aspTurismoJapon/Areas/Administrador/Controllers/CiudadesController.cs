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
    public class CiudadesController : Controller
    {
        public IHostingEnvironment Environment { get; set; }

        public CiudadesController(IHostingEnvironment env)
        {
            Environment = env;
        }

        public IActionResult Index()
        {
            CiudadesRepository ciudadesRepository = new CiudadesRepository();
            var ciudadesIEnumerable = ciudadesRepository.GetAll();
            return View(ciudadesIEnumerable);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Ciudades_ViewModel ciudades_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CiudadesRepository ciudadesRepository = new CiudadesRepository();
                    var ciudadResult = ciudadesRepository.GetCiudadesByNombre(ciudades_ViewModel.Nombre);

                    if (ciudadResult == null)
                    {
                        ciudadesRepository.InsertCiudadesViewModel(ciudades_ViewModel);

                        if (ciudades_ViewModel.PortadaFile == null)
                        {
                            ciudadesRepository.SetNOPhoto(ciudades_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (ciudades_ViewModel.PortadaFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG.");
                            return View(ciudades_ViewModel);
                        }
                        else if (ciudades_ViewModel.PortadaFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            return View(ciudades_ViewModel);
                        }
                        else
                        {
                            ciudadesRepository.SetPhoto(ciudades_ViewModel.Id, ciudades_ViewModel.PortadaFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("Ciudades", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe una ciudad con este nombre");
                        return View(ciudades_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(ciudades_ViewModel);
                }
            }
            else
            {
                return View(ciudades_ViewModel);
            }
        }

        public IActionResult Editar(int id)
        {
            CiudadesRepository ciudadesRepository = new CiudadesRepository();
            var ciudadResult = ciudadesRepository.GetCiudadesById(id);
            return View(ciudadResult);
        }

        [HttpPost]
        public IActionResult Editar(Ciudades_ViewModel ciudades_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CiudadesRepository ciudadesRepository = new CiudadesRepository();
                    var ciudadResult = ciudadesRepository.GetCiudadesByNombre(ciudades_ViewModel.Nombre);

                    if (ciudadResult == null)
                    {
                        ciudadesRepository.UpdateCiudadesViewModel(ciudades_ViewModel);

                        if (ciudades_ViewModel.PortadaFile == null)
                        {
                            //ciudadesRepository.SetNOPhoto(ciudades_ViewModel.Id, Environment.WebRootPath);
                        }
                        else if (ciudades_ViewModel.PortadaFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG.");
                            return View(ciudades_ViewModel);
                        }
                        else if (ciudades_ViewModel.PortadaFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            return View(ciudades_ViewModel);
                        }
                        else
                        {
                            ciudadesRepository.SetPhoto(ciudades_ViewModel.Id, ciudades_ViewModel.PortadaFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("Ciudades", "Administrador");
                    }
                    else if (ciudadResult.Id == ciudades_ViewModel.Id)
                    {
                        ciudadResult.Nombre = ciudades_ViewModel.Nombre;
                        ciudadResult.Contenido = ciudades_ViewModel.Contenido;

                        ciudadesRepository.Update(ciudadResult);

                        if (ciudades_ViewModel.PortadaFile == null)
                        {
                            //ciudadesRepository.SetNOPhoto(ciudades_ViewModel.Id, Environment.WebRootPath);
                        }
                        else if (ciudades_ViewModel.PortadaFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG.");
                            return View(ciudades_ViewModel);
                        }
                        else if (ciudades_ViewModel.PortadaFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            return View(ciudades_ViewModel);
                        }
                        else
                        {
                            ciudadesRepository.SetPhoto(ciudades_ViewModel.Id, ciudades_ViewModel.PortadaFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("Ciudades", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe una ciudad con este nombre");
                        return View(ciudades_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(ciudades_ViewModel);
                }
            }
            else
            {
                return View(ciudades_ViewModel);
            }
        }

        public IActionResult Eliminar(int id)
        {
            CiudadesRepository ciudadesRepository = new CiudadesRepository();
            var ciudadResult = ciudadesRepository.GetById(id);
            return View(ciudadResult);
        }

        [HttpPost]
        public IActionResult Eliminar(Ciudades ciudades)
        {
            CiudadesRepository ciudadesRepository = new CiudadesRepository();
            var ciudadResult = ciudadesRepository.GetById(ciudades.Id);

            if (ciudadResult == null)
            {
                ModelState.AddModelError("", "Esta ciudad no existe o ya fue eliminada");
                return View(ciudadResult);
            }
            else
            {
                ciudadesRepository.Delete(ciudadResult);
                return RedirectToAction("Ciudades", "Administrador");
            }
        }
    }
}