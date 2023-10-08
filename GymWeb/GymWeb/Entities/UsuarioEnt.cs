namespace GymWeb.Entities
{
    public class UsuarioEnt
    {

        public int IdUsuario { get; set; }

        public string Identificacion { get; set; } = string.Empty;

        public string NombreCompleto { get; set; } = string.Empty;

        public int IdRol { get; set; }

        public string Correo { get; set; } = string.Empty;

        public string contrasenna { get; set; } = string.Empty;


        public int Telefono { get; set; }

        public bool ClaveTemporal { get; set; }

        public DateTime FechaCreacionUsuario { get; set; }

        public DateTime FechaCaducidad { get; set; }
        public bool estado { get; set; }

    }
}
