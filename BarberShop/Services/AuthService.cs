using BarberShop.Models;

namespace BarberShop.Services
{
    public interface IAuthService
    {
        Task<(bool success, string message, AuthUser? user)> LoginAsync(string roleName, string password);
        Task LogoutAsync();
        Task<AuthUser?> GetCurrentUserAsync();
        bool IsAuthenticated();
    }

    // Implementación del servicio de autenticación
    public class AuthService : IAuthService
    {
        private AuthUser? _currentUser;
        private readonly IRoleService _roleService;

        // Inyectamos el servicio de roles para validar las credenciales
        public AuthService(IRoleService roleService)
        {
            _roleService = roleService;
        }
        // Método de login que valida el rol y la contraseña
        public async Task<(bool success, string message, AuthUser? user)> LoginAsync(string roleName, string password)
        {
            var roles = await _roleService.GetAllRolesAsync();
            var role = roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));

            if (role == null)
            {
                return (false, "El rol no existe", null);
            }

            if (!role.Password.Equals(password))
            {
                return (false, "Contraseña incorrecta", null);
            }

            _currentUser = new AuthUser
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Description = role.Description,
                LoginTime = DateTime.Now
            };

            return (true, "Login exitoso", _currentUser);
        }
        // Método de logout que simplemente limpia el usuario actual
        public Task LogoutAsync()
        {
            _currentUser = null;
            return Task.CompletedTask;
        }

        public Task<AuthUser?> GetCurrentUserAsync()
        {
            return Task.FromResult(_currentUser);
        }

        public bool IsAuthenticated()
        {
            return _currentUser != null;
        }
    }
}
