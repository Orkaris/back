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
        private readonly IDataRepositoryInterTable<SessionExercise> dataRepositorySessionExercise;
        private readonly IDataRepository<ExerciseGoal> dataRepositoryExerciseGoal;
        private readonly IDataRepository<Exercise> dataRepositoryExercise;
        private readonly IMapper _mapper;

        public SessionController(IDataRepositoryGetAllById<Session> dataRepository, IDataRepositoryInterTable<SessionExercise> dataRepositorySessionExercise, IDataRepository<ExerciseGoal> dataRepositoryExerciseGoal, IDataRepository<Exercise> dataRepositoryExercise, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            this.dataRepositorySessionExercise = dataRepositorySessionExercise;
            this.dataRepositoryExerciseGoal = dataRepositoryExerciseGoal;
            this.dataRepositoryExercise = dataRepositoryExercise;
            _mapper = mapper;
        }
        //[Authorize]
        [HttpGet("ByWorkoutId/{workoutId}")]
        // [AuthorizeUserMatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SessionDTO>>> GetSessions(Guid workoutId)
        {
            return Ok(_mapper.Map<IEnumerable<SessionDTO>>((await dataRepository.GetAllByIdAsync(workoutId)).Value));
        }
        [HttpGet("ByUserId/{userId}")]
        // [AuthorizeUserMatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SessionDTO>>> GetSessionsByUserId(Guid userId)
        {
            return Ok(_mapper.Map<IEnumerable<SessionDTO>>((await dataRepository.GetAllByIdAsync2(userId)).Value));
        }
        //[Authorize]
        // [AuthorizeUserMatch("userId")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SessionDTO>> GetSessionById(Guid id)
        {
            var session = await dataRepository.GetByIdAsync(id);
            await dataRepositorySessionExercise.GetAllByIdAsync(id);
            await dataRepositoryExerciseGoal.GetAllAsync();
            await dataRepositoryExercise.GetAllAsync();

            if (session == null)
            {
                return NotFound();
            }

            return _mapper.Map<SessionDTO>(session.Value);
        }

        //[Authorize]
        [HttpPost]
        // [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SessionDTO>> PostSession(PostSessionDTO SessionDTO)
        {
            var Session = _mapper.Map<Session>(SessionDTO);
            await dataRepository.AddAsync(Session);

            return CreatedAtAction(nameof(GetSessionById), new { id = Session.Id, userId = Session.UserId }, _mapper.Map<SessionDTO>(Session));
        }
        //[Authorize]
        [HttpPost("PostSession2")]
        // [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SessionDTO>> PostSession2(PostSession2DTO sessionDTO)
        {
            var sessionToPost = _mapper.Map<PostSessionDTO>(sessionDTO);

            var session = _mapper.Map<Session>(_mapper.Map<PostSessionDTO>(sessionDTO));
            await dataRepository.AddAsync(session);
            foreach (var exerciseGoalDTO in sessionDTO.SessionExerciseSession)
            {
                var exerciseGoal = _mapper.Map<ExerciseGoal>(exerciseGoalDTO);
                await dataRepositoryExerciseGoal.AddAsync(exerciseGoal);
                await dataRepositorySessionExercise.AddAsync(new SessionExercise
                {
                    SessionId = session.Id,
                    ExerciseId = exerciseGoal.Id
                });

            }

            return CreatedAtAction(nameof(GetSessionById), new { id = session.Id, userId = session.UserId }, _mapper.Map<SessionDTO>(session));
        }


        //[Authorize]
        [HttpDelete("{id}")]
        // [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSession(Guid id)
        {
            var session = await dataRepository.GetByIdAsync(id);
            if (session.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(session.Value!);

            return NoContent();
        }

        //[Authorize]
        [HttpPut("{id}")]
        // [AuthorizeUserMatch("userId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSession(Guid id, PutSessionDTO sessionDTO)
        {
            var sessionToPost = _mapper.Map<PostSessionDTO>(sessionDTO);
            var existingSession = await dataRepository.GetByIdAsync(id);
            if (existingSession.Value == null)
            {
                return NotFound();
            }
            IEnumerable<SessionExerciseDTO> existingSessionExercise = _mapper.Map<IEnumerable<SessionExerciseDTO>>((await dataRepositorySessionExercise.GetAllByIdAsync(id)).Value);
            var exerciseGoalIdsToKeep = sessionDTO.ExercisesGoal.Select(eg => eg.Id).ToHashSet();
            var exerciseGoalsToDelete = new List<ExerciseGoal>();
            foreach (var sessionExercice in existingSessionExercise)
            {
                if (sessionExercice != null && !exerciseGoalIdsToKeep.Contains(sessionExercice.ExerciseId))
                {
                    var sessionExerciseEntity = (await dataRepositorySessionExercise.GetByIds(id, sessionExercice.ExerciseId)).Value;
                    if (sessionExerciseEntity != null)
                    {
                        await dataRepositorySessionExercise.DeleteAsync(sessionExerciseEntity);
                    }
                    exerciseGoalsToDelete.Add(_mapper.Map<ExerciseGoal>(sessionExercice.ExerciseGoalSessionExercise));
                }
            }
            foreach (var exerciseGoal in exerciseGoalsToDelete)
            {
                var sessionExerciseEntity = (await dataRepositoryExerciseGoal.GetByIdAsync(exerciseGoal.Id)).Value;
                if (sessionExerciseEntity != null)
                {
                    await dataRepositoryExerciseGoal.DeleteAsync(sessionExerciseEntity);
                }
            }
            foreach (var exerciseGoalDTO in sessionDTO.ExercisesGoal)
            {
                var exerciseGoal = _mapper.Map<ExerciseGoalDTO>(exerciseGoalDTO);
                var existingExerciseGoal = await dataRepositoryExerciseGoal.GetByIdAsync(exerciseGoal.Id);
                if (existingExerciseGoal.Value == null)
                {
                    return NotFound("Exercise goal not found: " + exerciseGoalDTO.Reps);
                }

                var exercise = _mapper.Map(exerciseGoal, existingExerciseGoal.Value);
                await dataRepositoryExerciseGoal.UpdateAsync(existingExerciseGoal.Value, exercise);
            }

            var session = _mapper.Map(sessionDTO, existingSession.Value);
            await dataRepository.UpdateAsync(existingSession.Value, session);

            return NoContent();
        }


    }
}
