using BarberShop.Models;

namespace BarberShop.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int id);
    }

    public class RoleService : IRoleService
    {
        private List<Role> _roles = new();
        private int _nextId = 1;

        public RoleService()
        {
            InitializeDefaultRoles();
        }

        private void InitializeDefaultRoles()
        {
            _roles = new List<Role>
            {
                new Role
                {
                    Id = _nextId++,
                    Name = "ADMIN",
                    WorkerName = "Juan Administrador", // Nuevo campo
                    Description = "Administrador del sistema con acceso total",
                    Password = "admin123",
                    CreatedAt = DateTime.Now
                },
                new Role
                {
                    Id = _nextId++,
                    Name = "BARBERO",
                    WorkerName = "Carlos Barbero", // Nuevo campo
                    Description = "Personal de barbería que realiza servicios",
                    Password = "barbero123",
                    CreatedAt = DateTime.Now
                },
                new Role
                {
                    Id = _nextId++,
                    Name = "VENDEDOR",
                    WorkerName = "Ana Ventas", // Nuevo campo
                    Description = "Personal de ventas y atención al cliente",
                    Password = "vendedor123",
                    CreatedAt = DateTime.Now
                }
            };
        }

        public Task<List<Role>> GetAllRolesAsync()
        {
            return Task.FromResult(_roles.OrderBy(r => r.Name).ToList());
        }

        public Task<Role?> GetRoleByIdAsync(int id)
        {
            return Task.FromResult(_roles.FirstOrDefault(r => r.Id == id));
        }

        public Task<Role> CreateRoleAsync(Role role)
        {
            role.Id = _nextId++;
            role.CreatedAt = DateTime.Now;
            _roles.Add(role);
            return Task.FromResult(role);
        }

        public Task<Role> UpdateRoleAsync(Role role)
        {
            var existingRole = _roles.FirstOrDefault(r => r.Id == role.Id);
            if (existingRole != null)
            {
                // Actualizamos todos los campos, incluyendo el nuevo
                existingRole.Name = role.Name;
                existingRole.WorkerName = role.WorkerName;
                existingRole.Description = role.Description;
                existingRole.Password = role.Password;
            }
            return Task.FromResult(role);
        }

        public Task<bool> DeleteRoleAsync(int id)
        {
            var role = _roles.FirstOrDefault(r => r.Id == id);
            if (role != null)
            {
                _roles.Remove(role);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}