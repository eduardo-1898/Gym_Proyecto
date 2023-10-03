﻿namespace GymAPI.Entities
{
    public class UsuarioEnt
    {

 

        public int   IdUsuario { get; set; }

        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }
        public int IdRol { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public int Telefono { get; set; }
        public bool ClaveTemporal { get; set; }
        public DateTime FechaCreacionUsuario { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public bool Estado { get; set; }





    }




    public class UsuarioEntRespuesta
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public UsuarioEnt? Objeto { get; set; } = null;
        public List<UsuarioEnt> Objetos { get; set; } = new List<UsuarioEnt>();
        public bool Resultado { get; set; }
    }



}
