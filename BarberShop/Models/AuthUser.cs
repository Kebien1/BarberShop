namespace BarberShop.Models
{
    public class AuthUser
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; }
    }
}
