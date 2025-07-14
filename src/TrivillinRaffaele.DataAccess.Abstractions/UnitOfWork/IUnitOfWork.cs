
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork.Repositories;

namespace TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork;

public interface IUnitOfWork
{
    ISensorRepository Categories { get; }
    ISensorDataRepository Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
