using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Models.ViewModels
{
    public class TipoAtraccion_ViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "EL tipo es obligatorio")]
        [MaxLength(45, ErrorMessage = "El tipo no puede pasar de 45 caracteres")]
        public string Tipo { get; set; }

        [NotMapped]
        public IFormFile IconoFile { get; set; }
    }
}
