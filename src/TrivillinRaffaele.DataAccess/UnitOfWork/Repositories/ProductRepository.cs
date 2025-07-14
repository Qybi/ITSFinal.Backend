using Microsoft.EntityFrameworkCore;
using Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork.Repositories;
using Cled.TrivillinRaffaeleEsame.DataAccess.Contexts;
using Cled.TrivillinRaffaeleEsame.Models.Entities;

namespace Cled.TrivillinRaffaeleEsame.DataAccess.UnitOfWork.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Product> GetByCodeAsync(string code)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.Code == code);
    }
}