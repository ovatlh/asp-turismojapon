using System;
using System.Collections.Generic;

namespace aspTurismoJapon.Models
{
    public partial class Ciudades
    {
        public Ciudades()
        {
            Atracciones = new HashSet<Atracciones>();
            Comidas = new HashSet<Comidas>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Portada { get; set; }
        public string Contenido { get; set; }

        public ICollection<Atracciones> Atracciones { get; set; }
        public ICollection<Comidas> Comidas { get; set; }
    }
}
