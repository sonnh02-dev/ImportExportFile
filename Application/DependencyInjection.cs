using ImportExportFile.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace ImportExportFile.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ImportService>();
            services.AddScoped<ExportService>();
            return services;
        }
    }
}
