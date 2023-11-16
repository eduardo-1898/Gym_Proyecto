namespace GymAPI.Entities
{
    public class EjercicioEnt
    {
        public int IdEjercicio { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaCreacionEjercicio { get; set; }
        public bool? Estado { get; set; }
    }
}
