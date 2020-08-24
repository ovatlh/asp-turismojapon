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
    public class AtraccionesController : Controller
    {
        public IHostingEnvironment Environment { get; set; }

        public AtraccionesController(IHostingEnvironment env)
        {
            Environment = env;
        }

        public IActionResult Index()
        {
            AtraccionesRepository atraccionesRepository = new AtraccionesRepository();
            var atraccionesIEnumerable = atraccionesRepository.GetAtraccionesConNavigation();
            return View(atraccionesIEnumerable);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Atracciones_ViewModel atracciones_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AtraccionesRepository atraccionesRepository = new AtraccionesRepository();
                    var atraccionResult = atraccionesRepository.GetAtraccionesByTitulo(atracciones_ViewModel.Titulo);

                    if (atraccionResult == null)
                    {
                        atraccionesRepository.InsertAtraccionesViewModel(atracciones_ViewModel);

                        if (atracciones_ViewModel.PortadaFile == null)
                        {
                            atraccionesRepository.SetNOPhoto(atracciones_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (atracciones_ViewModel.PortadaFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG");
                            return View(atracciones_ViewModel);
                        }
                        else if (atracciones_ViewModel.PortadaFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            return View(atracciones_ViewModel);
                        }
                        else
                        {
                            atraccionesRepository.SetPhoto(atracciones_ViewModel.Id, atracciones_ViewModel.PortadaFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("Atracciones", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe una atraccion con este titulo");
                        return View(atracciones_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(atracciones_ViewModel);
                }
            }
            else
            {
                return View(atracciones_ViewModel);
            }
        }

        public IActionResult Editar(int id)
        {
            AtraccionesRepository atraccionesRepository = new AtraccionesRepository();
            var atraccionResult = atraccionesRepository.GetAtraccionesById(id);
            return View(atraccionResult);
        }

        [HttpPost]
        public IActionResult Editar(Atracciones_ViewModel atracciones_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AtraccionesRepository atraccionesRepository = new AtraccionesRepository();
                    var atraccionResult = atraccionesRepository.GetAtraccionesByTitulo(atracciones_ViewModel.Titulo);

                    if (atraccionResult == null)
                    {
                        atraccionesRepository.UpdateAtraccionesViewModel(atracciones_ViewModel);

                        if (atracciones_ViewModel.PortadaFile == null)
                        {
                            //atraccionesRepository.SetNOPhoto(atracciones_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (atracciones_ViewModel.PortadaFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG");
                            return View(atracciones_ViewModel);
                        }
                        else if (atracciones_ViewModel.PortadaFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            return View(atracciones_ViewModel);
                        }
                        else
                        {
                            atraccionesRepository.SetPhoto(atracciones_ViewModel.Id, atracciones_ViewModel.PortadaFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("Atracciones", "Administrador");
                    }
                    else if (atraccionResult.Id == atracciones_ViewModel.Id)
                    {
                        atraccionResult.Titulo = atracciones_ViewModel.Titulo;
                        atraccionResult.Contenido = atracciones_ViewModel.Contenido;
                        atraccionResult.IdTipo = atracciones_ViewModel.IdTipo;
                        atraccionResult.IdCiudad = atracciones_ViewModel.IdCiudad;

                        atraccionesRepository.Update(atraccionResult);

                        if (atracciones_ViewModel.PortadaFile == null)
                        {
                            //atraccionesRepository.SetNOPhoto(atracciones_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (atracciones_ViewModel.PortadaFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG");
                            return View(atracciones_ViewModel);
                        }
                        else if (atracciones_ViewModel.PortadaFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            return View(atracciones_ViewModel);
                        }
                        else
                        {
                            atraccionesRepository.SetPhoto(atracciones_ViewModel.Id, atracciones_ViewModel.PortadaFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("Atracciones", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe una atraccion con este titulo");
                        return View(atracciones_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(atracciones_ViewModel);
                }
            }
            else
            {
                return View(atracciones_ViewModel);
            }
        }

        public IActionResult Eliminar(int id)
        {
            AtraccionesRepository atraccionesRepository = new AtraccionesRepository();
            var atraccionResult = atraccionesRepository.GetById(id);
            return View(atraccionResult);
        }

        [HttpPost]
        public IActionResult Eliminar(Atracciones atracciones)
        {
            AtraccionesRepository atraccionesRepository = new AtraccionesRepository();
            var atraccionResult = atraccionesRepository.GetById(atracciones.Id);

            if (atraccionResult == null)
            {
                ModelState.AddModelError("", "Esta atraccion no existe o ya fue eliminada");
                return View(atraccionResult);
            }
            else
            {
                atraccionesRepository.Delete(atraccionResult);
                return RedirectToAction("Atracciones", "Administrador");
            }
        }
    }
}