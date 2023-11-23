using GymWeb.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymWeb.Models
{

    public interface IUsuarioModel
    {
        public UsuarioEntRespuesta? InicioSesion(UsuarioEnt entidad);
        public int RegistrarUsuario(UsuarioEnt entidad);
        public int RecuperarCuenta(UsuarioEnt entidad);
        public int AgregarFotoPerfil(byte[] img, int id);
        public UsuarioEnt ModificarInfoPerfil(UsuarioEnt entidad);
        public UsuarioEntRespuesta? ConsultarUsuarios();


        Task<int> ActualizarEstadoUsuario(int IdUsuario, bool estado);

        public UsuarioEntRespuesta? ConsultarUsuario(long q);

        public UsuarioEntRespuesta? ModificarUsuario(UsuarioEnt entidad);

    }
}

