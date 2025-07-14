using Microsoft.Extensions.DependencyInjection;
using Cled.TrivillinRaffaeleEsame.DataAccess.UnitOfWork;
using Cled.TrivillinRaffaeleEsame.DataAccess.UnitOfWork.Repositories;
using Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork;
using Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork.Repositories;

namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
