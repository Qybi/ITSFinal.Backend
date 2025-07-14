using Cled.TrivillinRaffaeleEsame.Models.Entities;

namespace Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product> GetByCodeAsync(string code);
}
