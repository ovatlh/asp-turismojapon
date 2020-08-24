using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Models.ViewModels
{
    public class Atracciones_ViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El titulo de la atraccion es obligatorio")]
        [MaxLength(45, ErrorMessage = "El titulo no puede pasar de 45 caracteres")]
        public string Titulo { get; set; }

        public string Portada { get; set; }

        [Required(ErrorMessage = "El contenido de la atraccion es obligatorio")]
        public string Contenido { get; set; }

        //[MinLength(1, ErrorMessage = "El tipo de atraccion es obligatorio")]
        [Range(1, 9999, ErrorMessage = "El tipo de atraccion es obligatorio")]
        public int IdTipo { get; set; }
        
        //[MinLength(1, ErrorMessage = "La ciudad es obligatoria")]
        [Range(1, 9999, ErrorMessage = "La ciudad es obligatoria")]
        public int IdCiudad { get; set; }

        [NotMapped]
        public IFormFile PortadaFile { get; set; }
    }
}
