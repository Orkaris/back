using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class ExerciseGoalPerformanceManager : IDataRepositoryGetAllById<ExerciseGoalPerformance>
{
    private readonly WorkoutDBContext _context;

    public ExerciseGoalPerformanceManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<ExerciseGoalPerformance>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<ExerciseGoalPerformance>>(await _context.ExerciseGoalPerformances.ToListAsync());
    }

    public async Task<ActionResult<ExerciseGoalPerformance>> GetByIdAsync(Guid id)
    {
        var ExerciseGoalPerformance = await _context.ExerciseGoalPerformances.FindAsync(id);
        if (ExerciseGoalPerformance == null) return new NotFoundResult();
        return new ActionResult<ExerciseGoalPerformance>(ExerciseGoalPerformance);
    }

    public async Task AddAsync(ExerciseGoalPerformance entity)
    {
        _context.ExerciseGoalPerformances.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExerciseGoalPerformance entityToUpdate, ExerciseGoalPerformance entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ExerciseGoalPerformance entity)
    {
        _context.ExerciseGoalPerformances.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<ActionResult<IEnumerable<ExerciseGoalPerformance>>> GetAllByIdAsync(Guid id)
    {
        return new ActionResult<IEnumerable<ExerciseGoalPerformance>>(await _context.ExerciseGoalPerformances.Where(w => w.ExerciseGoalId == id).ToListAsync());

    }

    public Task<ActionResult<IEnumerable<ExerciseGoalPerformance>>> GetAllByIdAsync2(Guid id)
    {
        throw new NotImplementedException();
    }
}
