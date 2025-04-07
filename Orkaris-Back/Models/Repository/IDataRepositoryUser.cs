using System;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Models.EntityFramework;

namespace Orkaris_Back.Models.Repository;

public interface IDataRepositoryUser : IDataRepository<User>
{
   Task<ActionResult<User>> GetByStringAsync(string str);
}