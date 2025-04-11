using System;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Models.EntityFramework;

namespace Orkaris_Back.Models.Repository;

public interface IDataRepositoryString<T> : IDataRepository<T>
{
   Task<ActionResult<T>> GetByStringAsync(string str);
}