namespace BarberShop.Models
{
    public class Role
    {
        public int Id { get; set; }

        // Este campo seguirá guardando el tipo de rol (Ej: ADMIN, BARBERO)
        public string Name { get; set; } = string.Empty;

        // Nuevo campo para el nombre del trabajador
        public string WorkerName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}