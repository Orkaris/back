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
        private readonly IDataRepository<ExerciseMuscleLink> dataRepositoryExerciseMuscleLink;
        private readonly IDataRepository<Muscle> dataRepositoryMuscle;

        public SessionController(
            IDataRepositoryGetAllById<Session> dataRepository,
            IDataRepositoryInterTable<SessionExercise> dataRepositorySessionExercise,
            IDataRepository<ExerciseGoal> dataRepositoryExerciseGoal,
            IDataRepository<Exercise> dataRepositoryExercise,
            IDataRepository<ExerciseMuscleLink> dataRepositoryExerciseMuscleLink,
            IDataRepository<Muscle> dataRepositoryMuscle,
            IMapper mapper
        )
        {
            this.dataRepository = dataRepository;
            this.dataRepositorySessionExercise = dataRepositorySessionExercise;
            this.dataRepositoryExerciseGoal = dataRepositoryExerciseGoal;
            this.dataRepositoryExercise = dataRepositoryExercise;
            this.dataRepositoryExerciseMuscleLink = dataRepositoryExerciseMuscleLink;
            this.dataRepositoryMuscle = dataRepositoryMuscle;
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
            var sessions = _mapper.Map<IEnumerable<SessionDTO>>((await dataRepository.GetAllByIdAsync2(userId)).Value);
            return Ok(sessions);
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
        public async Task<IActionResult> PutSession(Guid id, PostSession2DTO sessionDTO)
        {
            var existingSession = await dataRepository.GetByIdAsync(id);
            if (existingSession.Value == null)
            {
                return NotFound();
            }

            // Supprimer les anciens exercise goals et leurs relations
            var existingSessionExercises = await dataRepositorySessionExercise.GetAllByIdAsync(id);
            if (existingSessionExercises.Value != null)
            {
                foreach (var sessionExercise in existingSessionExercises.Value)
                {
                    // D'abord supprimer la relation
                    await dataRepositorySessionExercise.DeleteAsync(sessionExercise);

                    // Ensuite supprimer l'exercise goal
                    var exerciseGoal = await dataRepositoryExerciseGoal.GetByIdAsync(sessionExercise.ExerciseId);
                    if (exerciseGoal.Value != null)
                    {
                        await dataRepositoryExerciseGoal.DeleteAsync(exerciseGoal.Value);
                    }
                }
            }

            // Mettre à jour les informations de base de la session
            var session = _mapper.Map(sessionDTO, existingSession.Value);
            await dataRepository.UpdateAsync(existingSession.Value, session);

            // Créer les nouveaux exercise goals et leurs relations
            foreach (var exerciseGoalDTO in sessionDTO.SessionExerciseSession)
            {
                // D'abord créer l'exercise goal
                var exerciseGoal = _mapper.Map<ExerciseGoal>(exerciseGoalDTO);
                await dataRepositoryExerciseGoal.AddAsync(exerciseGoal);

                // Créer la relation avec l'ID de l'exercise goal créé
                await dataRepositorySessionExercise.AddAsync(new SessionExercise
                {
                    SessionId = session.Id,
                    ExerciseId = exerciseGoal.Id
                });
            }

            return NoContent();
        }

        [HttpGet("{sessionId}/muscles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<object>>> GetMusclesBySession(Guid sessionId)
        {
            // 1. Récupère tous les SessionExercise pour la session
            var sessionExercisesResult = await dataRepositorySessionExercise.GetAllByIdAsync(sessionId);
            var sessionExercises = sessionExercisesResult.Value?.ToList();
            if (sessionExercises == null || !sessionExercises.Any())
                return NotFound("Aucun exercice trouvé pour cette session.");

            // 2. Pour chaque SessionExercise, récupérer l'ExerciseGoal (exg_id)
            var exerciseGoalIds = sessionExercises.Select(se => se.ExerciseId).ToList();

            // 3. Récupérer les ExerciseGoal correspondants
            var allExerciseGoalsResult = await dataRepositoryExerciseGoal.GetAllAsync();
            var allExerciseGoals = allExerciseGoalsResult.Value?.ToList() ?? new List<ExerciseGoal>();
            var relevantGoals = allExerciseGoals.Where(eg => exerciseGoalIds.Contains(eg.Id)).ToList();

            // 4. Récupérer les ExerciseId associés
            var exerciseIds = relevantGoals.Select(eg => eg.ExerciseId).Distinct().ToList();

            // 5. Récupérer les liens exercice-muscle
            var allLinksResult = await dataRepositoryExerciseMuscleLink.GetAllAsync();
            var allLinks = allLinksResult.Value?.ToList() ?? new List<ExerciseMuscleLink>();
            var relevantLinks = allLinks.Where(l => exerciseIds.Contains(l.ExerciseId)).ToList();

            // 6. Récupérer les muscles associés
            var muscleIds = relevantLinks.Select(l => l.MuscleId).Distinct().ToList();
            var allMusclesResult = await dataRepositoryMuscle.GetAllAsync();
            var allMuscles = allMusclesResult.Value?.ToList() ?? new List<Muscle>();
            var muscleDict = allMuscles.Where(m => muscleIds.Contains(m.Id)).ToDictionary(m => m.Id, m => m);

            // 7. Accumuler les stats par muscle
            var muscleStats = new Dictionary<string, (int nbRep, int nbSet)>();

            foreach (var goal in relevantGoals)
            {
                // Pour chaque exercice lié à ce goal, trouver les muscles
                var links = relevantLinks.Where(l => l.ExerciseId == goal.ExerciseId);
                foreach (var link in links)
                {
                    if (!muscleDict.TryGetValue(link.MuscleId, out var muscle))
                        continue;

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

            // 8. Appliquer une formule sur les reps/sets pour chaque muscle
            var result = muscleStats.Select(ms => new
            {
                nomMuscle = ms.Key,
                valeur = ms.Value.nbRep * ms.Value.nbSet // 🧠 Formule : adapte ici si besoin
            }).ToList();

            return Ok(result);
        }

        [HttpDelete("{sessionId}/exercise-goal/{exerciseGoalId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExerciseGoalFromSession(Guid sessionId, Guid exerciseGoalId)
        {
            // Vérifier si la session existe
            var session = await dataRepository.GetByIdAsync(sessionId);
            if (session.Value == null)
            {
                return NotFound("Session not found");
            }

            // Vérifier si l'exercise goal existe
            var exerciseGoal = await dataRepositoryExerciseGoal.GetByIdAsync(exerciseGoalId);
            if (exerciseGoal.Value == null)
            {
                return NotFound("Exercise goal not found");
            }

            // Récupérer la relation session-exercise goal
            var sessionExercise = await dataRepositorySessionExercise.GetByIds(sessionId, exerciseGoalId);
            if (sessionExercise.Value == null)
            {
                return NotFound("Exercise goal is not associated with this session");
            }

            // Supprimer la relation
            await dataRepositorySessionExercise.DeleteAsync(sessionExercise.Value);

            // Supprimer l'exercise goal
            await dataRepositoryExerciseGoal.DeleteAsync(exerciseGoal.Value);

            return NoContent();
        }

    }
}
