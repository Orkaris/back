using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Models.DTO;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;
using Orkaris_Back.Services;

namespace Orkaris_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IDataRepository<Workout> dataRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public WorkoutController(IDataRepository<Workout> dataRepository, IMapper mapper, JwtService jwtService)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        // [Authorize]
        [HttpGet("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WorkoutDTO>> GetWorkout(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null)
            {
                return Forbid();
            }

            var workout = await dataRepository.GetByIdAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            return _mapper.Map<WorkoutDTO>(workout.Value);
        }
    }
}
