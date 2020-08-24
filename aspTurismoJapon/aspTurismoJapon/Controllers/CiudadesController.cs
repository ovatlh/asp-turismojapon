using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspTurismoJapon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aspTurismoJapon.Controllers
{
    public class CiudadesController : Controller
    {

        [Route("Ciudades/")]
        public IActionResult Index()
        {
            CiudadesRepository ciudadesRepository = new CiudadesRepository();
            var ciudadesIEnumerable = ciudadesRepository.GetAll();
            return View(ciudadesIEnumerable);
        }

        [Route("Ciudad/{id}")]
        public IActionResult Ciudad(string Id)
        {
            CiudadesRepository ciudadesRepository = new CiudadesRepository();
            var ciudadResult = ciudadesRepository.GetCiudadesByNombreConNavigation(Id.Replace("_"," "));

            if (ciudadResult == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(ciudadResult);
            }
        }
    }
}