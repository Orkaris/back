using System;
using Microsoft.AspNetCore.Mvc;

namespace Orkaris_Back.Models.Repository;

public interface IDataRepositoryInterTable<T> : IDataRepositoryGetAllById<T>
{
    Task<ActionResult<T>> GetByIds(Guid sessionId, Guid exerciseId);

}
