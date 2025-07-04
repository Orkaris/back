using System;
﻿using Microsoft.AspNetCore.Mvc;
namespace Orkaris_Back.Models.Repository;

public interface IDataRepository<TEntity>
{
    Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
    Task<ActionResult<TEntity>> GetByIdAsync(Guid id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
    Task DeleteAsync(TEntity entity);
}
