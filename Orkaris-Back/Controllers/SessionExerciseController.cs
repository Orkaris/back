using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Models.DTO;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionExerciseController : ControllerBase
    {
        private readonly IDataRepositoryInterTable<SessionExercise> dataRepository;
        private readonly IMapper _mapper;

        public SessionExerciseController(IDataRepositoryInterTable<SessionExercise> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }
        
        //[Authorize]
        [HttpGet("{sessionId}/{exerciseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SessionExerciseDTO>> GetSessionExerciseByIds(Guid sessionId, Guid exerciseId)
        {
            var sessionExercise = await dataRepository.GetByIds(sessionId, exerciseId);

            if (sessionExercise == null)
            {
                return NotFound();
            }

            return _mapper.Map<SessionExerciseDTO>(sessionExercise.Value);
        }
        //[Authorize]
        [HttpGet("BySessionId/{sessionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SessionExerciseDTO>>> GetSessionExercisesBySessionId(Guid sessionId)
        {
            var sessionExercise = await dataRepository.GetAllByIdAsync(sessionId);

            if (sessionExercise == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<SessionExerciseDTO>>((await dataRepository.GetAllByIdAsync(sessionId)).Value));
        }



        //[Authorize]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SessionExerciseDTO>> PostSessionExercise(PostSessionExerciseDTO sessionExerciseDTO)
        {
            var sessionExercise = _mapper.Map<SessionExercise>(sessionExerciseDTO);
            await dataRepository.AddAsync(sessionExercise);

            return CreatedAtAction(nameof(GetSessionExerciseByIds), new { sessionId = sessionExercise.SessionId, exerciseId = sessionExercise.ExerciseId}, _mapper.Map<SessionExerciseDTO>(sessionExercise));
        }

        
        //[Authorize]
        [HttpDelete("{sessionId}/{exerciseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSessionExercise(Guid sessionId, Guid exerciseId)
        {
            var sessionExercise = await dataRepository.GetByIds(sessionId, exerciseId);
            if (sessionExercise.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(sessionExercise.Value!);

            return NoContent();
        }
        

        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSessionExercise(Guid id, PostSessionExerciseDTO sessionExerciseDTO)
        {
            var existingSessionExercise = await dataRepository.GetByIdAsync(id);
            if (existingSessionExercise.Value == null)
            {
                return NotFound();
            }

            var sessionExercise = _mapper.Map(sessionExerciseDTO, existingSessionExercise.Value);
            await dataRepository.UpdateAsync(existingSessionExercise.Value, sessionExercise);

            return NoContent();
        }
    }
}
