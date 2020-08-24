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
    public class TipoAtraccionController : Controller
    {
        public IHostingEnvironment Environment { get; set; }

        public TipoAtraccionController(IHostingEnvironment env)
        {
            Environment = env;
        }

        public IActionResult Index()
        {
            TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();
            var tipoAtraccionIEnumerable = tipoAtraccionRepository.GetAll();
            return View(tipoAtraccionIEnumerable);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(TipoAtraccion_ViewModel tipoAtraccion_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();
                    var tipoAtraccionResult = tipoAtraccionRepository.GetTipoAtraccionByTipo(tipoAtraccion_ViewModel.Tipo);

                    if (tipoAtraccionResult == null)
                    {
                        tipoAtraccionRepository.InsertTipoAtraccionViewModel(tipoAtraccion_ViewModel);

                        if (tipoAtraccion_ViewModel.IconoFile == null)
                        {
                            tipoAtraccionRepository.SetNOPhoto(tipoAtraccion_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (tipoAtraccion_ViewModel.IconoFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG.");
                            return View(tipoAtraccion_ViewModel);
                        }
                        else if (tipoAtraccion_ViewModel.IconoFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            return View(tipoAtraccion_ViewModel);
                        }
                        else
                        {
                            tipoAtraccionRepository.SetPhoto(tipoAtraccion_ViewModel.Id, tipoAtraccion_ViewModel.IconoFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("TipoAtraccion", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe este tipo de atraccion");
                        return View(tipoAtraccion_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(tipoAtraccion_ViewModel);
                }
            }
            else
            {
                return View(tipoAtraccion_ViewModel);
            }
        }

        public IActionResult Editar(int id)
        {
            TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();
            var tipoAtraccionResult = tipoAtraccionRepository.GetTipoAtraccionById(id);
            return View(tipoAtraccionResult);
        }

        [HttpPost]
        public IActionResult Editar(TipoAtraccion_ViewModel tipoAtraccion_ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();
                    var tipoAtraccionResult = tipoAtraccionRepository.GetTipoAtraccionByTipo(tipoAtraccion_ViewModel.Tipo);

                    if (tipoAtraccionResult == null)
                    {
                        tipoAtraccionRepository.UpdateTipoAtraccionViewModel(tipoAtraccion_ViewModel);

                        if (tipoAtraccion_ViewModel.IconoFile == null)
                        {
                            //tipoAtraccionRepository.SetNOPhoto(tipoAtraccion_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (tipoAtraccion_ViewModel.IconoFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG.");
                            return View(tipoAtraccion_ViewModel);
                        }
                        else if (tipoAtraccion_ViewModel.IconoFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            return View(tipoAtraccion_ViewModel);
                        }
                        else
                        {
                            tipoAtraccionRepository.SetPhoto(tipoAtraccion_ViewModel.Id, tipoAtraccion_ViewModel.IconoFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("TipoAtraccion", "Administrador");
                    }
                    else if (tipoAtraccionResult.Id == tipoAtraccion_ViewModel.Id)
                    {
                        tipoAtraccionResult.Tipo = tipoAtraccion_ViewModel.Tipo;

                        tipoAtraccionRepository.Update(tipoAtraccionResult);

                        if (tipoAtraccion_ViewModel.IconoFile == null)
                        {
                            //tipoAtraccionRepository.SetNOPhoto(tipoAtraccion_ViewModel.Id, $"{Environment.WebRootPath}/images/");
                        }
                        else if (tipoAtraccion_ViewModel.IconoFile.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("", "Solo se pueden cargar imagenes JPG.");
                            return View(tipoAtraccion_ViewModel);
                        }
                        else if (tipoAtraccion_ViewModel.IconoFile.Length > 1024 * 1024)
                        {
                            ModelState.AddModelError("", "El tamaño maximo de una imagen es de [ 1 MB ].");
                            return View(tipoAtraccion_ViewModel);
                        }
                        else
                        {
                            tipoAtraccionRepository.SetPhoto(tipoAtraccion_ViewModel.Id, tipoAtraccion_ViewModel.IconoFile, $"{Environment.WebRootPath}/images/");
                        }

                        return RedirectToAction("TipoAtraccion", "Administrador");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ya existe este tipo de atraccion");
                        return View(tipoAtraccion_ViewModel);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(tipoAtraccion_ViewModel);
                }
            }
            else
            {
                return View(tipoAtraccion_ViewModel);
            }
        }

        public IActionResult Eliminar(int id)
        {
            TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();
            var tipoAtraccionResult = tipoAtraccionRepository.GetById(id);
            return View(tipoAtraccionResult);
        }

        [HttpPost]
        public IActionResult Eliminar(Tipoatraccion tipoAtraccion)
        {
            TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();
            var tipoAtraccionResult = tipoAtraccionRepository.GetById(tipoAtraccion.Id);

            if (tipoAtraccionResult == null)
            {
                ModelState.AddModelError("", "Este tipo de atraccion no existe o ya fue eliminado");
                return View(tipoAtraccionResult);
            }
            else
            {
                tipoAtraccionRepository.Delete(tipoAtraccionResult);
                return RedirectToAction("TipoAtraccion", "Administrador");
            }
        }
    }
}