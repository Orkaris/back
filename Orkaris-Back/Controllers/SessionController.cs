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
            try
            {
                // Validation des données
                if (sessionDTO == null)
                {
                    return BadRequest("Session data cannot be null");
                }

                if (sessionDTO.SessionExerciseSession == null || !sessionDTO.SessionExerciseSession.Any())
                {
                    return BadRequest("Session must contain at least one exercise");
                }

                // Vérification de la session existante
                var existingSession = await dataRepository.GetByIdAsync(id);
                if (existingSession.Value == null)
                {
                    return NotFound("Session not found");
                }

                try
                {
                    // Mise à jour de la session de base
                    var updatedSession = _mapper.Map<Session>(sessionDTO);
                    updatedSession.Id = existingSession.Value.Id; // S'assurer que l'ID est préservé
                    await dataRepository.UpdateAsync(existingSession.Value, updatedSession);

                    // Récupération des anciens exercise goals
                    var existingSessionExercises = await dataRepositorySessionExercise.GetAllByIdAsync(id);
                    var existingExerciseGoals = new List<ExerciseGoal>();

                    if (existingSessionExercises.Value != null)
                    {
                        try
                        {
                            // Récupération des exercise goals existants
                            var exerciseGoalIds = existingSessionExercises.Value.Select(se => se.ExerciseId).ToList();
                            var allExerciseGoals = await dataRepositoryExerciseGoal.GetAllAsync();
                            existingExerciseGoals = allExerciseGoals.Value?
                                .Where(eg => exerciseGoalIds.Contains(eg.Id))
                                .ToList() ?? new List<ExerciseGoal>();

                            // Suppression des relations
                            foreach (var sessionExercise in existingSessionExercises.Value)
                            {
                                await dataRepositorySessionExercise.DeleteAsync(sessionExercise);
                            }

                            // Suppression des exercise goals
                            foreach (var exerciseGoal in existingExerciseGoals)
                            {
                                await dataRepositoryExerciseGoal.DeleteAsync(exerciseGoal);
                            }
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(500, $"Error while deleting existing exercise goals: {ex.Message}\nInner exception: {ex.InnerException?.Message}");
                        }
                    }

                    try
                    {
                        // Création des nouveaux exercise goals
                        var newExerciseGoals = sessionDTO.SessionExerciseSession
                            .Select(dto => _mapper.Map<ExerciseGoal>(dto))
                            .ToList();

                        foreach (var exerciseGoal in newExerciseGoals)
                        {
                            await dataRepositoryExerciseGoal.AddAsync(exerciseGoal);
                            await dataRepositorySessionExercise.AddAsync(new SessionExercise
                            {
                                SessionId = existingSession.Value.Id,
                                ExerciseId = exerciseGoal.Id
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error while creating new exercise goals: {ex.Message}\nInner exception: {ex.InnerException?.Message}");
                    }

                    return NoContent();
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception";
                    return StatusCode(500, $"Error during session update: {ex.Message}\nInner exception: {innerExceptionMessage}\nStack trace: {ex.StackTrace}");
                }
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception";
                return StatusCode(500, $"An error occurred while updating the session: {ex.Message}\nInner exception: {innerExceptionMessage}\nStack trace: {ex.StackTrace}");
            }
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
