using System;
using System.Collections.Generic;

namespace aspTurismoJapon.Models
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
    }
}
