using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Models.DTO;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseGoalPerformanceController : ControllerBase
    {
        private readonly IDataRepositoryGetAllById<ExerciseGoalPerformance> dataRepository;
        private readonly IMapper _mapper;

        public ExerciseGoalPerformanceController(IDataRepositoryGetAllById<ExerciseGoalPerformance> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ExerciseGoalPerformanceDTO>>> GetExerciseGoalPerformanceById(Guid id)
        {

            var exerciseGoalPerformance = await dataRepository.GetByIdAsync(id);

            if (exerciseGoalPerformance == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ExerciseGoalPerformanceDTO>>(exerciseGoalPerformance.Value));
        }

        [HttpGet("ByExerciseGoal/{exerciseGoalId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ExerciseGoalPerformanceDTO>>> GetExerciseGoalPerformanceByExerciseGoalId(Guid exerciseGoalId)
        {
            return Ok(_mapper.Map<IEnumerable<ExerciseGoalPerformanceDTO>>((await dataRepository.GetAllByIdAsync(exerciseGoalId)).Value));
    
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExerciseGoalPerformanceDTO>> PostExerciseGoalPerformance(PostExerciseGoalPerformanceDTO exerciseGoalPerformanceDTO)
        {
            var exerciseGoalPerformance = _mapper.Map<ExerciseGoalPerformance>(exerciseGoalPerformanceDTO);
            await dataRepository.AddAsync(exerciseGoalPerformance);

            return CreatedAtAction(nameof(GetExerciseGoalPerformanceById), new { id = exerciseGoalPerformance.Id }, _mapper.Map<ExerciseGoalPerformanceDTO>(exerciseGoalPerformance));
        }


        //[Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExerciseGoalPerformance(Guid id)
        {
            var exerciseGoalPerformance = await dataRepository.GetByIdAsync(id);
            if (exerciseGoalPerformance.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(exerciseGoalPerformance.Value!);

            return NoContent();
        }

        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutExerciseGoalPerformance(Guid id, PostExerciseGoalPerformanceDTO exerciseGoalPerformanceDTO)
        {
            var existingExerciseGoalPerformance = await dataRepository.GetByIdAsync(id);
            if (existingExerciseGoalPerformance.Value == null)
            {
                return NotFound();
            }

            var exerciseGoalPerformance = _mapper.Map(exerciseGoalPerformanceDTO, existingExerciseGoalPerformance.Value);
            await dataRepository.UpdateAsync(existingExerciseGoalPerformance.Value, exerciseGoalPerformance);

            return NoContent();
        }
    }
}
