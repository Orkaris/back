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

        [HttpGet("{sessionId}/muscles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<object>>> GetMusclesBySession(Guid sessionId)
        {
            // 1. Récupère tous les SessionExercise pour la session (table de jointure : t_j_session_exercise_goal_seg)
            var sessionExercisesResult = await dataRepositorySessionExercise.GetAllByIdAsync(sessionId);
            var sessionExercises = sessionExercisesResult.Value?.ToList();
            if (sessionExercises == null || !sessionExercises.Any())
                return NotFound("Aucun exercice trouvé pour cette session.");

            // 2. Pour chaque SessionExercise, récupérer l'ExerciseGoal (exg_id)
            var exerciseGoalIds = sessionExercises.Select(se => se.ExerciseId).ToList();

            // 3. Pour chaque ExerciseGoal, récupérer l'ExerciseId (exr_id), Sets et Reps
            var allExerciseGoalsResult = await dataRepositoryExerciseGoal.GetAllAsync();
            var allExerciseGoals = allExerciseGoalsResult.Value?.ToList() ?? new List<ExerciseGoal>();
            var relevantGoals = allExerciseGoals.Where(eg => exerciseGoalIds.Contains(eg.Id)).ToList();

            // 4. Pour chaque Exercise, récupérer les muscles associés
            var exerciseIds = relevantGoals.Select(eg => eg.ExerciseId).Distinct().ToList();
            var allExercisesResult = await dataRepositoryExercise.GetAllAsync();
            var allExercises = allExercisesResult.Value?.ToList() ?? new List<Exercise>();
            var exerciseDict = allExercises.Where(e => exerciseIds.Contains(e.Id)).ToDictionary(e => e.Id, e => e);

            // 5. Pour chaque muscle, additionner les reps/sets de tous les ExerciseGoal de la session
            var muscleStats = new Dictionary<string, (int nbRep, int nbSet)>();

            foreach (var goal in relevantGoals)
            {
                if (!exerciseDict.TryGetValue(goal.ExerciseId, out var exercise))
                    continue;

                foreach (var muscle in exercise.Muscles)
                {
                    if (muscleStats.ContainsKey(muscle.Name))
                    {
                        var prev = muscleStats[muscle.Name];
                        muscleStats[muscle.Name] = (prev.nbRep + goal.Reps, prev.nbSet + goal.Sets);
                    }
                    else
                    {
                        muscleStats[muscle.Name] = (goal.Reps, goal.Sets);
                    }
                }
            }

            var result = muscleStats.Select(ms => new {
                nomMuscle = ms.Key,
                nbRep = ms.Value.nbRep,
                nbSet = ms.Value.nbSet
            }).ToList();

            return Ok(result);
        }


    }
}
