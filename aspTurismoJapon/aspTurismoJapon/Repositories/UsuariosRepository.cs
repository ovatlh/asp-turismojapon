using aspTurismoJapon.Models;
using aspTurismoJapon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspTurismoJapon.Repositories
{
    public class UsuariosRepository : Repository<Usuarios>
    {
        public Usuarios GetUsuariosByNombre(string nombre)
        {
            return GetAll().FirstOrDefault(x => x.Nombre == nombre);
        }

        public void InsertUsuariosViewModel(Usuarios_ViewModel usuarios_ViewModel)
        {
            Usuarios usuarios = new Usuarios()
            {
                Nombre = usuarios_ViewModel.Nombre,
                Contrasena = usuarios_ViewModel.Contrasena,
                Rol = usuarios_ViewModel.Rol
            };

            Insert(usuarios);
        }

        public void UpdateUsuariosViewModel(Usuarios_ViewModel usuarios_ViewModel)
        {
            var usuarioResult = GetById(usuarios_ViewModel.Id);
            if (usuarioResult != null)
            {
                usuarioResult.Nombre = usuarios_ViewModel.Nombre;
                usuarioResult.Contrasena = usuarios_ViewModel.Contrasena;
                usuarioResult.Rol = usuarios_ViewModel.Rol;

                Update(usuarioResult);
            }
        }

        public Usuarios_ViewModel GetUsuariosById(int id)
        {
            return Context.Usuarios
                .Select(x => new Usuarios_ViewModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Contrasena = x.Contrasena,
                    Rol = x.Rol
                }).FirstOrDefault(x => x.Id == id);
        }
    }
}
