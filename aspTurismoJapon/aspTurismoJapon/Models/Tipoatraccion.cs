using System;
using System.Collections.Generic;

namespace aspTurismoJapon.Models
{
    public partial class Tipoatraccion
    {
        public Tipoatraccion()
        {
            Atracciones = new HashSet<Atracciones>();
        }

        public int Id { get; set; }
        public string Tipo { get; set; }

        public ICollection<Atracciones> Atracciones { get; set; }
    }
}
