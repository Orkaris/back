using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Models.DataManager;

public class EmailManager : IDataRepositoryString<EmailConfirmationToken>
{
    private readonly WorkoutDBContext _context;

    public EmailManager(WorkoutDBContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<EmailConfirmationToken>>> GetAllAsync()
    {
        return new ActionResult<IEnumerable<EmailConfirmationToken>>(await _context.EmailConfirmationTokens.ToListAsync());
    }

    public async Task<ActionResult<EmailConfirmationToken>> GetByIdAsync(Guid id)
    {
        var user = await _context.EmailConfirmationTokens.FindAsync(id);
        if (user == null) return new NotFoundResult();
        return new ActionResult<EmailConfirmationToken>(user);
    }

    public async Task AddAsync(EmailConfirmationToken entity)
    {
        _context.EmailConfirmationTokens.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EmailConfirmationToken entityToUpdate, EmailConfirmationToken entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(EmailConfirmationToken entity)
    {
        _context.EmailConfirmationTokens.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ActionResult<EmailConfirmationToken>> GetByStringAsync(string str)
    {
        var user = await _context.EmailConfirmationTokens.FirstOrDefaultAsync(u => u.Token == str);
        if (user == null) return new NotFoundResult();
        return new ActionResult<EmailConfirmationToken>(user);
    }
}