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
    public class AtraccionesRepository : Repository<Atracciones>
    {
        public IEnumerable<Atracciones> GetAtraccionesConNavigation()
        {
            return Context.Atracciones.Include(x => x.IdCiudadNavigation).Include(x => x.IdTipoNavigation);
        }

        public Atracciones GetAtraccionesByTitulo(string titulo)
        {
            //return GetAll().FirstOrDefault(x => x.Titulo == titulo);
            return Context.Atracciones
                .Include(x => x.IdCiudadNavigation)
                .Include(x => x.IdTipoNavigation)
                .FirstOrDefault(x => x.Titulo == titulo);
        }

        public IEnumerable<Atracciones> GetAtraccionesConNavitagionByTipo(string tipo)
        {
            return GetAtraccionesConNavigation().Where(x => x.IdTipoNavigation.Tipo == tipo);
        }

        public void InsertAtraccionesViewModel(Atracciones_ViewModel atracciones_ViewModel)
        {
            Atracciones atracciones = new Atracciones()
            {
                Titulo = atracciones_ViewModel.Titulo,
                Contenido = atracciones_ViewModel.Contenido,
                IdTipo = atracciones_ViewModel.IdTipo,
                IdCiudad = atracciones_ViewModel.IdCiudad
            };

            Insert(atracciones);
            atracciones_ViewModel.Id = atracciones.Id;
        }

        public void UpdateAtraccionesViewModel(Atracciones_ViewModel atracciones_ViewModel)
        {
            var atraccionResult = GetById(atracciones_ViewModel.Id);

            if (atraccionResult != null)
            {
                atraccionResult.Titulo = atracciones_ViewModel.Titulo;
                atraccionResult.Contenido = atracciones_ViewModel.Contenido;
                atraccionResult.IdTipo = atracciones_ViewModel.IdTipo;
                atraccionResult.IdCiudad = atracciones_ViewModel.IdCiudad;

                Update(atraccionResult);
            }
        }

        public void SetNOPhoto(int id, string path)
        {
            var origen = Path.Combine(path, "atracciones", "nophoto.jpg");
            var desitno = Path.Combine(path, "atracciones", $"{id}.jpg");

            File.Copy(origen, desitno);
        }

        public void SetPhoto(int id, IFormFile foto, string path)
        {
            var ruta = Path.Combine(path, "atracciones", $"{id}.jpg");
            FileStream fs = File.Create(ruta);
            foto.CopyTo(fs);
            fs.Close();
        }

        public Atracciones_ViewModel GetAtraccionesById(int id)
        {
            return Context.Atracciones
                .Select(x => new Atracciones_ViewModel
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Contenido = x.Contenido,
                    IdTipo = x.IdTipo,
                    IdCiudad = x.IdCiudad
                }).FirstOrDefault(x => x.Id == id);
        }
    }
}
