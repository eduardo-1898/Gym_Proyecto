using GymWeb.Entities;
using static GymWeb.Entities.UsuarioEnt;

namespace GymWeb.Interfaces
{
    public interface IUsuarioModel
    {
        public UsuarioEntRespuesta? InicioSesion(UsuarioEnt entidad);



    }
}