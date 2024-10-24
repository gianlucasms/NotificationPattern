using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLayeredArchitecture.Domain.Repositories;
using NewLayeredArchitecture.Infra.Context;
using NewLayeredArchitecture.Infra.Repositories;

namespace NewLayeredArchitecture.DependencyInjection;

public static class DependencyInjectionConfig
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}

