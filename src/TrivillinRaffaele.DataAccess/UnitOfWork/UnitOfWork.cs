using Microsoft.Extensions.DependencyInjection;
using TrivillinRaffaele.DataAccess.Abstractions.Contexts;
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork;
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork.Repositories;

namespace TrivillinRaffaele.DataAccess.UnitOfWork
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

        private ISensorRepository? _categoryRepository;
        private ISensorDataRepository? _productRepository;
        public ISensorRepository Categories => _categoryRepository ??= _serviceProvider.GetRequiredService<ISensorRepository>();
        public ISensorDataRepository Products => _productRepository ??= _serviceProvider.GetRequiredService<ISensorDataRepository>();
        
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
