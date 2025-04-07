using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class SessionPerformanceManager : IDataRepository<SessionPerformance>
{
    private readonly WorkoutDBContext _context;

    public SessionPerformanceManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<SessionPerformance>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<SessionPerformance>>(await _context.SessionPerformances.ToListAsync());
    }

    public async Task<ActionResult<SessionPerformance>> GetByIdAsync(Guid id)
    {
        var SessionPerformance = await _context.SessionPerformances.FindAsync(id);
        if (SessionPerformance == null) return new NotFoundResult();
        return new ActionResult<SessionPerformance>(SessionPerformance);
    }

    public async Task AddAsync(SessionPerformance entity)
    {
        _context.SessionPerformances.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SessionPerformance entityToUpdate, SessionPerformance entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SessionPerformance entity)
    {
        _context.SessionPerformances.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
