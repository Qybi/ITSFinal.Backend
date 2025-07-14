
using Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork.Repositories;

namespace Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
