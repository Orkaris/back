using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager
{
    public class ExerciseMuscleLinkManager : IDataRepository<ExerciseMuscleLink>
    {
        private readonly WorkoutDBContext _context;

        public ExerciseMuscleLinkManager(WorkoutDBContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<ExerciseMuscleLink>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<ExerciseMuscleLink>>(await _context.Set<ExerciseMuscleLink>().ToListAsync());
        }

        public Task<ActionResult<ExerciseMuscleLink>> GetByIdAsync(Guid id)
        {
            // Not applicable for composite key, return NotFound
            return Task.FromResult<ActionResult<ExerciseMuscleLink>>(new NotFoundResult());
        }

        public async Task AddAsync(ExerciseMuscleLink entity)
        {
            _context.Set<ExerciseMuscleLink>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExerciseMuscleLink entityToUpdate, ExerciseMuscleLink entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ExerciseMuscleLink entity)
        {
            _context.Set<ExerciseMuscleLink>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
