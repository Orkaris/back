using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Attribute;
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
        private readonly IDataRepositoryGetAllById<Workout> dataRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public WorkoutController(IDataRepositoryGetAllById<Workout> dataRepository, IMapper mapper, JwtService jwtService)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        [Authorize]
        [AuthorizeUserMatch]
        [HttpGet("ByUserId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetWorkout(Guid id)
        {

            return Ok(_mapper.Map<IEnumerable<WorkoutDTO>>((await dataRepository.GetAllByIdAsync(id)).Value));
        }

        [AllowAnonymous]
        [HttpPost("{userId}")]
        [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WorkoutDTO>> PostWorkout(Guid userId, PostWorkoutDTO workoutDTO)
        {
            var workout = _mapper.Map<Workout>(workoutDTO);
            workout.UserId = userId;
            await dataRepository.AddAsync(workout);

            return CreatedAtAction(nameof(GetWorkout), new { id = workout.Id }, _mapper.Map<WorkoutDTO>(workout));
        }

    }
}
