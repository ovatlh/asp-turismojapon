using System;
using System.Collections.Generic;

namespace aspTurismoJapon.Models
{
    public partial class Comidas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Portada { get; set; }
        public string Descripcion { get; set; }
        public int IdCiudad { get; set; }

        public Ciudades IdCiudadNavigation { get; set; }
    }
}
