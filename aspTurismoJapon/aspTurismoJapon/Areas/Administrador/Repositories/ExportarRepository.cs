using aspTurismoJapon.Areas.Administrador.Models;
using aspTurismoJapon.Areas.Administrador.Models.ViewModels;
using aspTurismoJapon.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Areas.Administrador.Repositories
{
    public class ExportarRepository : Repository<ExportarViewModel>
    {
        public IEnumerable<reporteDatosCiudades> GetReporteDatos_Ciudades()
        {
            return Context.Ciudades
                .OrderBy(x => x.Nombre)
                .Select(x => new reporteDatosCiudades
                {
                    Nombre = x.Nombre
                }).ToList();
        }

        public IEnumerable<reporteDatosComidas> GetReporteDatos_Comidas()
        {
            return Context.Comidas
                .Include(x => x.IdCiudadNavigation)
                .OrderBy(x => x.Nombre)
                .Select(x => new reporteDatosComidas
                {
                    Nombre = x.Nombre,
                    Ciudad = x.IdCiudadNavigation.Nombre
                }).ToList();
        }

        public IEnumerable<reporteDatosTipoAtracciones> GetReporteDatos_TipoAtracciones()
        {
            return Context.Tipoatraccion
                .OrderBy(x => x.Tipo)
                .Select(x => new reporteDatosTipoAtracciones
                {
                    Tipo = x.Tipo
                }).ToList();
        }

        public IEnumerable<reporteDatosAtracciones> GetReporteDatos_Atracciones()
        {
            return Context.Atracciones
                .Include(x => x.IdTipoNavigation)
                .Include(x => x.IdCiudadNavigation)
                .OrderBy(x => x.Titulo)
                .Select(x => new reporteDatosAtracciones
                {
                    Titulo = x.Titulo,
                    Ciudad = x.IdCiudadNavigation.Nombre,
                    Tipo = x.IdTipoNavigation.Tipo
                }).ToList();
        }
    }
}
