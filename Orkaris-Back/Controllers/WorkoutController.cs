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
        private readonly IDataRepositoryGetAllById<Session> dataRepositorySession;
        private readonly IMapper _mapper;

        public WorkoutController(IDataRepositoryGetAllById<Workout> dataRepository, IDataRepositoryGetAllById<Session> dataRepositorySession, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            this.dataRepositorySession = dataRepositorySession;
            _mapper = mapper;
        }
        //[Authorize]
        // [AuthorizeUserMatch]
        [HttpGet("ByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetWorkouts(Guid userId)
        {

            return Ok(_mapper.Map<IEnumerable<WorkoutDTO>>((await dataRepository.GetAllByIdAsync(userId)).Value));
        }

        //[Authorize]
        // [AuthorizeUserMatch("userId")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WorkoutDTO>> GetWorkoutById(Guid id)
        {
            var workout = await dataRepository.GetByIdAsync(id);

            await dataRepositorySession.GetAllByIdAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            return _mapper.Map<WorkoutDTO>(workout.Value);
        }

        //[Authorize]
        [HttpPost]
        // [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WorkoutDTO>> GetWorkout(PostWorkoutDTO workoutDTO)
        {
            var workout = _mapper.Map<Workout>(workoutDTO);
            await dataRepository.AddAsync(workout);

            return CreatedAtAction(nameof(GetWorkoutById), new { id = workout.Id, userId = workout.UserId }, _mapper.Map<WorkoutDTO>(workout));
        }


        //[Authorize]
        [HttpDelete("{id}")]
        // [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWorkout(Guid id)
        {
            var workout = await dataRepository.GetByIdAsync(id);
            if (workout.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(workout.Value!);

            return NoContent();
        }

        //[Authorize]
        [HttpPut("{id}")]
        // [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutWorkout(Guid id, PostWorkoutDTO workoutDTO)
        {
            var existingWorkout = await dataRepository.GetByIdAsync(id);

            if (existingWorkout.Value == null)
            {
                return NotFound();
            }

            var workout = _mapper.Map(workoutDTO, existingWorkout.Value);
            await dataRepository.UpdateAsync(existingWorkout.Value, workout);

            return NoContent();
        }

    }
}
