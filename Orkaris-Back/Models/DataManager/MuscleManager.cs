using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager
{
    public class MuscleManager : IDataRepository<Muscle>
    {
        private readonly WorkoutDBContext _context;

        public MuscleManager(WorkoutDBContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Muscle>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<Muscle>>(await _context.Set<Muscle>().ToListAsync());
        }

        public async Task<ActionResult<Muscle>> GetByIdAsync(Guid id)
        {
            var muscle = await _context.Set<Muscle>().FindAsync(id);
            if (muscle == null) return new NotFoundResult();
            return new ActionResult<Muscle>(muscle);
        }

        public async Task AddAsync(Muscle entity)
        {
            _context.Set<Muscle>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Muscle entityToUpdate, Muscle entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Muscle entity)
        {
            _context.Set<Muscle>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
