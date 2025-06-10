using System;
using Microsoft.AspNetCore.Mvc;

namespace Orkaris_Back.Models.Repository;

public interface IDataRepositoryGetAllById<T> : IDataRepository<T>
{
    Task<ActionResult<IEnumerable<T>>> GetAllByIdAsync(Guid id);
    Task<ActionResult<IEnumerable<T>>> GetAllByIdAsync2(Guid id);
}
