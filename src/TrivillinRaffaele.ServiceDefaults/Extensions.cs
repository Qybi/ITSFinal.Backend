using Microsoft.Extensions.DependencyInjection;
using TrivillinRaffaele.DataAccess.UnitOfWork;
using TrivillinRaffaele.DataAccess.UnitOfWork.Repositories;
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork;
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork.Repositories;

namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ISensorRepository, SensorRepository>();
        services.AddScoped<ISensorDataRepository, SensorDataRepository>();

        return services;
    }
}
