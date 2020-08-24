using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Models.ViewModels
{
    public class Ciudades_ViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la ciudad es obligatorio")]
        [MaxLength(45,ErrorMessage = "El nombre no puede pasar de 45 caracteres")]
        public string Nombre { get; set; }
    
        public string Portada { get; set; }

        [Required(ErrorMessage = "El contenido de la ciudad es obligatorio")]
        public string Contenido { get; set; }

        [NotMapped]
        public IFormFile PortadaFile { get; set; }
    }
}
