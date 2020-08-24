using aspTurismoJapon.Models;
using aspTurismoJapon.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Repositories
{
    public class CiudadesRepository : Repository<Ciudades>
    {
        public Ciudades GetCiudadesByNombre(string nombre)
        {
            return GetAll().FirstOrDefault(x => x.Nombre == nombre);
        }

        public void InsertCiudadesViewModel(Ciudades_ViewModel ciudades_ViewModel)
        {
            Ciudades ciudades = new Ciudades()
            {
                Nombre = ciudades_ViewModel.Nombre,
                Contenido = ciudades_ViewModel.Contenido,
            };

            Insert(ciudades);
            ciudades_ViewModel.Id = ciudades.Id;
        }

        public void UpdateCiudadesViewModel(Ciudades_ViewModel ciudades_ViewModel)
        {
            var ciudadResult = GetById(ciudades_ViewModel.Id);
            if (ciudadResult != null)
            {
                ciudadResult.Nombre = ciudades_ViewModel.Nombre;
                ciudadResult.Contenido = ciudades_ViewModel.Contenido;

                Update(ciudadResult);
            }
        }

        public void SetNOPhoto(int id, string path)
        {
            var origen = Path.Combine(path, "ciudades", "nophoto.jpg");
            var desitno = Path.Combine(path, "ciudades", $"{id}.jpg");

            File.Copy(origen, desitno);
        }

        public void SetPhoto(int id, IFormFile foto, string path)
        {
            var ruta = Path.Combine(path, "ciudades", $"{id}.jpg");
            FileStream fs = File.Create(ruta);
            foto.CopyTo(fs);
            fs.Close();
        }

        public Ciudades_ViewModel GetCiudadesById(int id)
        {
            return Context.Ciudades
                .Select(x => new Ciudades_ViewModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Contenido = x.Contenido,
                }).FirstOrDefault(x => x.Id == id);
        }

        public Ciudades GetCiudadesByNombreConNavigation(string nombre)
        {
            return Context.Ciudades
                .Include(x => x.Atracciones)
                .Include(x => x.Comidas)
                .FirstOrDefault(x => x.Nombre == nombre);
        }
    }
}
