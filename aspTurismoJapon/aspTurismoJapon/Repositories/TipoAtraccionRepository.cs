using aspTurismoJapon.Models;
using aspTurismoJapon.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Repositories
{
    public class TipoAtraccionRepository : Repository<Tipoatraccion>
    {
        public Tipoatraccion GetTipoAtraccionByTipo(string tipo)
        {
            return GetAll().FirstOrDefault(x => x.Tipo == tipo);
        }

        public void InsertTipoAtraccionViewModel(TipoAtraccion_ViewModel tipoAtraccion_ViewModel)
        {
            Tipoatraccion tipoatraccion = new Tipoatraccion()
            {
                Tipo = tipoAtraccion_ViewModel.Tipo
            };

            Insert(tipoatraccion);
            tipoAtraccion_ViewModel.Id = tipoatraccion.Id;
        }

        public void UpdateTipoAtraccionViewModel(TipoAtraccion_ViewModel tipoAtraccion_ViewModel)
        {
            var tipoAtraccionResult = GetById(tipoAtraccion_ViewModel.Id);
            if (tipoAtraccionResult != null)
            {
                tipoAtraccionResult.Tipo = tipoAtraccion_ViewModel.Tipo;

                Update(tipoAtraccionResult);
            }
        }

        public void SetNOPhoto(int id, string path)
        {
            var origen = Path.Combine(path, "tipoatraccion", "nophoto.jpg");
            var desitno = Path.Combine(path, "tipoatraccion", $"{id}.jpg");

            File.Copy(origen, desitno);
        }

        public void SetPhoto(int id, IFormFile foto, string path)
        {
            var ruta = Path.Combine(path, "tipoatraccion", $"{id}.jpg");
            FileStream fs = File.Create(ruta);
            foto.CopyTo(fs);
            fs.Close();
        }

        public TipoAtraccion_ViewModel GetTipoAtraccionById(int id)
        {
            return Context.Tipoatraccion
                .Select(x => new TipoAtraccion_ViewModel
                {
                    Id = x.Id,
                    Tipo = x.Tipo
                }).FirstOrDefault(x => x.Id == id);
        }
    }
}
