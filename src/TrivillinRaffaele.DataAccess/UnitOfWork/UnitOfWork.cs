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

        private ISensorRepository? _sensorRepository;
        private ISensorDataRepository? _sensorDataRepository;
        public ISensorRepository Sensors => _sensorRepository ??= _serviceProvider.GetRequiredService<ISensorRepository>();
        public ISensorDataRepository SensorsData => _sensorDataRepository ??= _serviceProvider.GetRequiredService<ISensorDataRepository>();
        
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
