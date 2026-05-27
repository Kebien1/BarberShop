using BarberShop;
using BarberShop.Authentication;
using BarberShop.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// 1. Registramos los servicios de roles y autenticación
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// 2. CORRECCIÓN AQUÍ: Registramos el CustomAuthenticationStateProvider de forma que 
// pueda ser inyectado tanto por su clase concreta como por su clase base.
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthenticationStateProvider>());

// 3. Agregamos el núcleo de autorización de Blazor
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();