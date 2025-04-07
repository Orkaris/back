using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class SportManager : IDataRepository<Sport>
{
    private readonly WorkoutDBContext _context;

    public SportManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<Sport>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<Sport>>(await _context.Sports.ToListAsync());
    }

    public async Task<ActionResult<Sport>> GetByIdAsync(Guid id)
    {
        var Sport = await _context.Sports.FindAsync(id);
        if (Sport == null) return new NotFoundResult();
        return new ActionResult<Sport>(Sport);
    }

    public async Task AddAsync(Sport entity)
    {
        _context.Sports.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sport entityToUpdate, Sport entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Sport entity)
    {
        _context.Sports.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
