using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class ExerciseManager : IDataRepository<Exercise>
{
    private readonly WorkoutDBContext _context;

    public ExerciseManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<Exercise>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<Exercise>>(await _context.Exercises.ToListAsync());
    }

    public async Task<ActionResult<Exercise>> GetByIdAsync(Guid id)
    {
        var Exercise = await _context.Exercises.FindAsync(id);
        if (Exercise == null) return new NotFoundResult();
        return new ActionResult<Exercise>(Exercise);
    }

    public async Task AddAsync(Exercise entity)
    {
        _context.Exercises.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Exercise entityToUpdate, Exercise entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Exercise entity)
    {
        _context.Exercises.Remove(entity);
        await _context.SaveChangesAsync();
    }

}
