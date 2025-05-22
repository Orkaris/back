using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class SessionController : ControllerBase
    {
        private readonly IDataRepositoryGetAllById<Session> dataRepository;
        private readonly IDataRepositoryGetAllById<Exercise> dataRepositoryExercise;
        private readonly IMapper _mapper;

        public SessionController(IDataRepositoryGetAllById<Session> dataRepository,IDataRepositoryGetAllById<Exercise> dataRepositoryExercise, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            this.dataRepositoryExercise = dataRepositoryExercise;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet("ByUserId/{id}/ByWorkoutId/{workoutId}")]
        [AuthorizeUserMatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SessionDTO>>> GetSessions(Guid id, Guid workoutId)
        {
            return Ok(_mapper.Map<IEnumerable<SessionDTO>>((await dataRepository.GetAllByIdAsync(workoutId)).Value));
        }
        [Authorize]
        [AuthorizeUserMatch("userId")]
        [HttpGet("ById/{id}/ByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SessionDTO>> GetSessionById(Guid id, Guid userId)
        {
            var session = await dataRepository.GetByIdAsync(id);
            await dataRepositoryExercise.GetAllByIdAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return _mapper.Map<SessionDTO>(session.Value);
        }

        [AllowAnonymous]
        [HttpPost("{userId}/{workoutId}")]
        [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SessionDTO>> PostSession(PostSessionDTO SessionDTO,Guid userId, Guid workoutId)
        {
            var Session = _mapper.Map<Session>(SessionDTO);
            Session.UserId = userId;
            Session.WorkoutId = workoutId;
            await dataRepository.AddAsync(Session);

            return CreatedAtAction(nameof(GetSessionById), new { id = Session.Id, userId = Session.UserId }, _mapper.Map<SessionDTO>(Session));
        }

        
        [Authorize]
        [HttpDelete("{id}/{userId}")]
        [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSession(Guid id, Guid userId)
        {
            var session = await dataRepository.GetByIdAsync(id);
            if (session.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(session.Value!);

            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}/{userId}")]
        [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSession(Guid id, PostSessionDTO sessionDTO, Guid userId)
        {
            var existingSession = await dataRepository.GetByIdAsync(id);
            if (existingSession.Value == null)
            {
                return NotFound();
            }

            var session = _mapper.Map(sessionDTO, existingSession.Value);
            await dataRepository.UpdateAsync(existingSession.Value, session);

            return NoContent();
        }


    }
}
