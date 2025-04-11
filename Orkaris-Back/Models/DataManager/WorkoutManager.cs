using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class WorkoutManager : IDataRepositoryGetAllById<Workout>
{
    private readonly WorkoutDBContext _context;

    public WorkoutManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<Workout>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<Workout>>(await _context.Workouts.ToListAsync());
    }

    public async Task<ActionResult<Workout>> GetByIdAsync(Guid id)
    {
        var Workout = await _context.Workouts.FindAsync(id);
        if (Workout == null) return new NotFoundResult();
        return new ActionResult<Workout>(Workout);
    }

    public async Task AddAsync(Workout entity)
    {
        _context.Workouts.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Workout entityToUpdate, Workout entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Workout entity)
    {
        _context.Workouts.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<Workout>>> GetAllByIdAsync(Guid id)
    {
        return new ActionResult<IEnumerable<Workout>>(await _context.Workouts.Where(w => w.UserId == id).ToListAsync());       
    }

}
