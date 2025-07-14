using Microsoft.EntityFrameworkCore;
using Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork.Repositories;
using Cled.TrivillinRaffaeleEsame.DataAccess.Contexts;
using Cled.TrivillinRaffaeleEsame.Models.Entities;

namespace Cled.TrivillinRaffaeleEsame.DataAccess.UnitOfWork.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Category>> GetWithProductsAsync(int id)
    {
        return await _context.Categories
            .Include(c => c.Products)
            .Where(c => c.Id == id)
            .ToListAsync();
    }
}
