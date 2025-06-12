using Biblioteca.Data.Context;
using Biblioteca.Data.Repository;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Notificacoes;
using Biblioteca.Domain.Services;

namespace Biblioteca.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DbContextApp>();
            services.AddScoped<IEditoraRepository, EditoraRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IEditoraService, EditoraService>();
            services.AddScoped<IAutorService, AutorService>();

            // services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


            return services;
        }
    }
}
