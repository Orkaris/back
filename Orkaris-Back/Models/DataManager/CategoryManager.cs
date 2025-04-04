using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class CategoryManager : IDataRepository<Category>
{
    private readonly WorkoutDBContext _context;

    public CategoryManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<Category>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<Category>>(await _context.Categorys.ToListAsync());
    }

    public async Task<ActionResult<Category>> GetByIdAsync(Guid id)
    {
        var Category = await _context.Categorys.FindAsync(id);
        if (Category == null) return new NotFoundResult();
        return new ActionResult<Category>(Category);
    }

    public async Task AddAsync(Category entity)
    {
        _context.Categorys.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entityToUpdate, Category entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category entity)
    {
        _context.Categorys.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
