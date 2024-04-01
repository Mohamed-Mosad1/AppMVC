using AppMVC.BLL;
using AppMVC.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AppMVC.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
