using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class ExerciseCategoryManager : IDataRepositoryInterTable<ExerciseCategory>
{
    private readonly WorkoutDBContext _context;

    public ExerciseCategoryManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<ExerciseCategory>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<ExerciseCategory>>(await _context.ExerciseCategorys.ToListAsync());
    }

    public async Task<ActionResult<ExerciseCategory>> GetByIdAsync(Guid id)
    {
        var ExerciseCategory = await _context.ExerciseCategorys.FindAsync(id);
        if (ExerciseCategory == null) return new NotFoundResult();
        return new ActionResult<ExerciseCategory>(ExerciseCategory);
    }

    public async Task AddAsync(ExerciseCategory entity)
    {
        _context.ExerciseCategorys.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExerciseCategory entityToUpdate, ExerciseCategory entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ExerciseCategory entity)
    {
        _context.ExerciseCategorys.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ActionResult<ExerciseCategory>> GetByIds(Guid exerciseId, Guid categoryId)
    {
        var exerciseCategory = await _context.ExerciseCategorys.FirstOrDefaultAsync(w => w.ExerciseId == exerciseId && w.CategoryId == categoryId);
        if (exerciseCategory == null)
            return new NotFoundResult();
        return new ActionResult<ExerciseCategory>(exerciseCategory);
    }

    public async Task<ActionResult<IEnumerable<ExerciseCategory>>> GetAllByIdAsync(Guid exerciseId)
    {
        return new ActionResult<IEnumerable<ExerciseCategory>>(await _context.ExerciseCategorys.Where(w => w.ExerciseId == exerciseId).ToListAsync());
    }

}
