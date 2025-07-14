using Microsoft.EntityFrameworkCore;
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork.Repositories;
using TrivillinRaffaele.DataAccess.Contexts;
using TrivillinRaffaele.Models.Entities;

namespace TrivillinRaffaele.DataAccess.UnitOfWork.Repositories;

public class SensorDataRepository : Repository<SensorData>, ISensorDataRepository
{
    private readonly ApplicationDbContext _context;
    public SensorDataRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}