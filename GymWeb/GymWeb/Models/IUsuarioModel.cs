using GymWeb.Entities;

namespace GymWeb.Models
{

    public interface IUsuarioModel
    {



        public UsuarioEntRespuesta? InicioSesion(UsuarioEnt entidad);

        public int RegistrarUsuario(UsuarioEnt entidad);


        public int RecuperarContrasenna(UsuarioEnt entidad);

        public int CambiarClaveCuenta(UsuarioEnt entidad);



    }
}

