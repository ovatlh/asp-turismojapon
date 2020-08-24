using System;
using System.Collections.Generic;

namespace aspTurismoJapon.Models
{
    public partial class Atracciones
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Portada { get; set; }
        public string Contenido { get; set; }
        public int IdTipo { get; set; }
        public int IdCiudad { get; set; }

        public Ciudades IdCiudadNavigation { get; set; }
        public Tipoatraccion IdTipoNavigation { get; set; }
    }
}
