using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class SessionExerciseManager : IDataRepositoryInterTable<SessionExercise>
{
    private readonly WorkoutDBContext _context;

    public SessionExerciseManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<SessionExercise>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<SessionExercise>>(await _context.SessionExercises.ToListAsync());
    }

    public async Task<ActionResult<SessionExercise>> GetByIdAsync(Guid sessionId)
    {
        var SessionExercise = await _context.SessionExercises.FirstOrDefaultAsync(w => w.SessionId == sessionId);
        if (SessionExercise == null) return new NotFoundResult();
        return new ActionResult<SessionExercise>(SessionExercise);
    }

    public async Task AddAsync(SessionExercise entity)
    {
        _context.SessionExercises.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SessionExercise entityToUpdate, SessionExercise entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SessionExercise entity)
    {
        _context.SessionExercises.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ActionResult<SessionExercise>> GetByIds(Guid sessionId, Guid exerciseId)
    {
        var sessionExercise = await _context.SessionExercises.FirstOrDefaultAsync(w => w.SessionId == sessionId && w.ExerciseId == exerciseId);
        if (sessionExercise == null)
            return new NotFoundResult();
        return new ActionResult<SessionExercise>(sessionExercise);
    }

    public async Task<ActionResult<IEnumerable<SessionExercise>>> GetAllByIdAsync(Guid sessionId)
    {
        return new ActionResult<IEnumerable<SessionExercise>>(await _context.SessionExercises.Where(w => w.SessionId == sessionId).ToListAsync());
    }
}
