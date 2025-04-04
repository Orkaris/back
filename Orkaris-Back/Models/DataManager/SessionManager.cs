using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class SessionManager : IDataRepository<Session>
{
    private readonly WorkoutDBContext _context;

    public SessionManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<Session>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<Session>>(await _context.Sessions.ToListAsync());
    }

    public async Task<ActionResult<Session>> GetByIdAsync(Guid id)
    {
        var Session = await _context.Sessions.FindAsync(id);
        if (Session == null) return new NotFoundResult();
        return new ActionResult<Session>(Session);
    }

    public async Task AddAsync(Session entity)
    {
        _context.Sessions.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Session entityToUpdate, Session entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Session entity)
    {
        _context.Sessions.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
