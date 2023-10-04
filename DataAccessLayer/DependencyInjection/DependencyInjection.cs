using DataAccessLayer.Application.Dtos;
using DataAccessLayer.Application.Repositories;
using DataAccessLayer.Application.Repositories.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Persistence.Context;
using DataAccessLayer.Persistence.FakerData;
using DataAccessLayer.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DataAccessLayer.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
          //  services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                         throw new ApplicationException("the Connection string is null");
                return new SqlConnectionFactory(connectionString);
            });
            services.AddTransient<FakeData>();
            services.AddTransient<ICoreAsDapperRepository<EmployeeInfo>, CoreAsDapperEmployeeRepository>();
            services.AddTransient<IDapperRepository<EmployeeInfo>, DapperEmployeeRepository>();
            services.AddTransient<ICoreRepository<EmployeeInfo>, CoreEmployeeRepository>();
            services.AddTransient<ICompiledRepository<EmployeeInfo>, CompiledEmployeeRepository>();
            return services;
        }


    }
}
