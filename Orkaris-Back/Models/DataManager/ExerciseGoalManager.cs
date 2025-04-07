using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class ExerciseGoalManager : IDataRepository<ExerciseGoal>
{
    private readonly WorkoutDBContext _context;

    public ExerciseGoalManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<ExerciseGoal>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<ExerciseGoal>>(await _context.ExerciseGoals.ToListAsync());
    }

    public async Task<ActionResult<ExerciseGoal>> GetByIdAsync(Guid id)
    {
        var ExerciseGoal = await _context.ExerciseGoals.FindAsync(id);
        if (ExerciseGoal == null) return new NotFoundResult();
        return new ActionResult<ExerciseGoal>(ExerciseGoal);
    }

    public async Task AddAsync(ExerciseGoal entity)
    {
        _context.ExerciseGoals.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExerciseGoal entityToUpdate, ExerciseGoal entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ExerciseGoal entity)
    {
        _context.ExerciseGoals.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
