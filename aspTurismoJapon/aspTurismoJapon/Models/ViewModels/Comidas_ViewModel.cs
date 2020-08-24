using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Models.ViewModels
{
    public class Comidas_ViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la comida es obligatorio")]
        [MaxLength(45, ErrorMessage = "El nombre no puede pasar de 45 caracteres")]
        public string Nombre { get; set; }

        public string Portada { get; set; }

        [Required(ErrorMessage = "La descripcion de la comida es obligatoria")]
        public string Descripcion { get; set; }

        //[MinLength(1, ErrorMessage = "La ciudad es obligatoria")]
        [Range(1, 9999, ErrorMessage = "La ciudad es obligatoria")]
        public int IdCiudad { get; set; }

        [NotMapped]
        public IFormFile PortadaFile { get; set; }
    }
}
