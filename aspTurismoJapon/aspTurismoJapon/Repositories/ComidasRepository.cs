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
    public class ComidasRepository : Repository<Comidas>
    {
        public IEnumerable<Comidas> GetComidasConNavigation()
        {
            return Context.Comidas.Include(x => x.IdCiudadNavigation);
        }

        public Comidas GetComidaByNombre(string nombre)
        {
            return GetAll().FirstOrDefault(x => x.Nombre == nombre);
        }

        public void InsertComidasViewModel(Comidas_ViewModel comidas_ViewModel)
        {
            Comidas comidas = new Comidas
            {
                Nombre = comidas_ViewModel.Nombre,
                Descripcion = comidas_ViewModel.Descripcion,
                IdCiudad = comidas_ViewModel.IdCiudad
            };

            Insert(comidas);
            comidas_ViewModel.Id = comidas.Id;
        }

        public void UpdateComidasViewModel(Comidas_ViewModel comidas_ViewModel)
        {
            var comidaResult = GetById(comidas_ViewModel.Id);
            if (comidaResult != null)
            {
                comidaResult.Nombre = comidas_ViewModel.Nombre;
                comidaResult.Descripcion = comidas_ViewModel.Descripcion;
                comidaResult.IdCiudad = comidas_ViewModel.IdCiudad;

                Update(comidaResult);
            }
        }

        public void SetNOPhoto(int id, string path)
        {
            var origen = Path.Combine(path, "comidas", "nophoto.jpg");
            var desitno = Path.Combine(path, "comidas", $"{id}.jpg");

            File.Copy(origen, desitno);
        }

        public void SetPhoto(int id, IFormFile foto, string path)
        {
            var ruta = Path.Combine(path, "comidas", $"{id}.jpg");
            FileStream fs = File.Create(ruta);
            foto.CopyTo(fs);
            fs.Close();
        }

        public Comidas_ViewModel GetComidasById(int id)
        {
            return Context.Comidas
                .Select(x => new Comidas_ViewModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    IdCiudad = x.IdCiudad
                }).FirstOrDefault(x => x.Id == id);
        }

        public Comidas GetComidasByNombreConNavigation(string nombre)
        {
            return Context.Comidas
                .Include(x => x.IdCiudadNavigation)
                .FirstOrDefault(x => x.Nombre == nombre);
        }
    }
}
