using Cled.TrivillinRaffaeleEsame.Models.Entities;

namespace Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetWithProductsAsync(int id);
}
