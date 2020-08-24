using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Areas.Administrador.Models.ViewModels
{
    public class ImportarViewModel
    {
        public int Tipo { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
