
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork.Repositories;

namespace TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork;

public interface IUnitOfWork
{
    ISensorRepository Sensors { get; }
    ISensorDataRepository SensorsData { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
