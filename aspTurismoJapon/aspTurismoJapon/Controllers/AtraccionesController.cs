using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspTurismoJapon.Models;
using aspTurismoJapon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aspTurismoJapon.Controllers
{
    public class AtraccionesController : Controller
    {
        [Route("Atracciones/")]
        [Route("Atracciones/Index")]
        [Route("Atracciones/Inicio")]
        [Route("Atracciones/{id}")]
        public IActionResult Index(string Id)
        {
            AtraccionesRepository atraccionesRepository = new AtraccionesRepository();
            TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();
            IEnumerable<Atracciones> atraccionesIEnumerable;
            if (Id == null)
            {
                atraccionesIEnumerable = atraccionesRepository.GetAtraccionesConNavigation();
                return View(atraccionesIEnumerable);
            }
            else if (tipoAtraccionRepository.GetTipoAtraccionByTipo(Id) == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.TipoAtraccionSeleccionado = Id;
                atraccionesIEnumerable = atraccionesRepository.GetAtraccionesConNavitagionByTipo(Id.Replace("_", " "));
                return View(atraccionesIEnumerable);
            }
        }

        [Route("Atraccion/{id}")]
        public IActionResult Atraccion(string Id)
        {
            AtraccionesRepository atraccionesRepository = new AtraccionesRepository();
            var atraccionResult = atraccionesRepository.GetAtraccionesByTitulo(Id.Replace("_", " "));

            if (atraccionResult == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(atraccionResult);
            }
        }
    }
}