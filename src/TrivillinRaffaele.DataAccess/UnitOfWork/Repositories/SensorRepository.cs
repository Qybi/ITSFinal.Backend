using Microsoft.EntityFrameworkCore;
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork.Repositories;
using TrivillinRaffaele.DataAccess.Contexts;
using TrivillinRaffaele.Models.Entities;

namespace TrivillinRaffaele.DataAccess.UnitOfWork.Repositories;

public class SensorRepository : Repository<Sensor>, ISensorRepository
{
    private readonly ApplicationDbContext _context;
    public SensorRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<Sensor> GetWithDataAsync(int id)
    {
        return _context.Sensors
            .Include(x => x.SensorData)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
