using GymWeb.Entities;

namespace GymWeb.Models
{

    public interface IUsuarioModel
    {



        public UsuarioEnt? IniciarSesion(UsuarioEnt entidad);

        public int RegistrarUsuario(UsuarioEnt entidad);


        public int RecuperarCuenta(UsuarioEnt entidad);





    }
}

