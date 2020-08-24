using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Models.ViewModels
{
    public class Usuarios_ViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(20, ErrorMessage = "El nombre no puede pasar de 20 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Rol { get; set; }
    }
}
