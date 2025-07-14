using TrivillinRaffaele.Models.Entities;

namespace TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork.Repositories;

public interface ISensorRepository : IRepository<Sensor>
{
    Task<Sensor> GetWithDataAsync(int id);
}
