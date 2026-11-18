using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OakERP.Infrastructure.Persistence;

namespace OakERP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OakErpDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }    
}

