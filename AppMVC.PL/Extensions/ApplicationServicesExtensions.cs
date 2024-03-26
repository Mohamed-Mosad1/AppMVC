using AppMVC.BLL.Interfaces;
using AppMVC.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AppMVC.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
