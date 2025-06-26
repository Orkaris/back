using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class SessionManager : IDataRepositoryGetAllById<Session>
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
        var session = await _context.Sessions.FindAsync(id);
        if (session == null) return new NotFoundResult();
        return new ActionResult<Session>(session);
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

    public async Task<ActionResult<IEnumerable<Session>>> GetAllByIdAsync(Guid id)
    {
        return new ActionResult<IEnumerable<Session>>(await _context.Sessions.Where(w => w.WorkoutId == id).ToListAsync());       
    }

    public async Task<ActionResult<IEnumerable<Session>>> GetAllByIdAsync2(Guid id)
    {
        List<Workout> workouts = await _context.Workouts.Where(w => w.UserId == id).ToListAsync();
        return new ActionResult<IEnumerable<Session>>(await _context.Sessions.Where(w => workouts.Select(wo => wo.Id).Contains(w.WorkoutId)).ToListAsync());
    }
}
