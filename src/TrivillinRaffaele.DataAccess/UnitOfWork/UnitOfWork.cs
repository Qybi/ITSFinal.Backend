using Microsoft.Extensions.DependencyInjection;
using Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.Contexts;
using Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork;
using Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.UnitOfWork.Repositories;

namespace Cled.TrivillinRaffaeleEsame.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(IApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        private ICategoryRepository? _categoryRepository;
        private IProductRepository? _productRepository;
        public ICategoryRepository Categories => _categoryRepository ??= _serviceProvider.GetRequiredService<ICategoryRepository>();
        public IProductRepository Products => _productRepository ??= _serviceProvider.GetRequiredService<IProductRepository>();
        
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
