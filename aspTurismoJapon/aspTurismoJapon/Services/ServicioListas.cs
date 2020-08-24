using aspTurismoJapon.Models;
using aspTurismoJapon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Services
{
    public class ServicioListas
    {
        public IEnumerable<Ciudades> GetAllCiudades()
        {
            CiudadesRepository ciudadesRepository = new CiudadesRepository();
            return ciudadesRepository.GetAll();
        }

        public IEnumerable<string> GetAllCiudades_Nombre()
        {
            CiudadesRepository ciudadesRepository = new CiudadesRepository();
            return ciudadesRepository.GetAll().Select(x => x.Nombre);
        }

        public IEnumerable<Tipoatraccion> GetAllTipoAtracciones()
        {
            TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();
            return tipoAtraccionRepository.GetAll();
        }

        public IEnumerable<string> GetAllTipoAtracciones_Tipo()
        {
            TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();
            return tipoAtraccionRepository.GetAll().Select(x => x.Tipo);
        }

        public IEnumerable<Comidas> GetAllComidas()
        {
            ComidasRepository comidasRepository = new ComidasRepository();
            return comidasRepository.GetAll();
        }

        public IEnumerable<string> GetAllComidas_Nombre()
        {
            ComidasRepository comidasRepository = new ComidasRepository();
            return comidasRepository.GetAll().Select(x => x.Nombre);
        }
    }
}
