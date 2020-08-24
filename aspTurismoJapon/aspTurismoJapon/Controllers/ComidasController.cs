using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspTurismoJapon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aspTurismoJapon.Controllers
{
    public class ComidasController : Controller
    {
        [Route("Comidas/")]
        public IActionResult Index()
        {
            ComidasRepository comidasRepository = new ComidasRepository();
            var comidasIEnumerable = comidasRepository.GetComidasConNavigation();
            return View(comidasIEnumerable);
        }

        [Route("Comida/{id}")]
        public IActionResult Comida(string Id)
        {
            ComidasRepository comidasRepository = new ComidasRepository();
            var comidaResult = comidasRepository.GetComidasByNombreConNavigation(Id.Replace("_", " "));

            if (comidaResult == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(comidaResult);
            }
        }
    }
}