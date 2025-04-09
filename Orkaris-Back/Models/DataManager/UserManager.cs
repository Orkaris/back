using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class UserManager : IDataRepositoryString<User>
{
    private readonly WorkoutDBContext _context;

    public UserManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<User>>(await _context.Users.ToListAsync());
    }

    public async Task<ActionResult<User>> GetByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return new NotFoundResult();
        return new ActionResult<User>(user);
    }

    public async Task AddAsync(User entity)
    {
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User entityToUpdate, User entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ActionResult<User>> GetByStringAsync(string str)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == str);
        if (user == null) return new NotFoundResult();
        return new ActionResult<User>(user);
    }
}
