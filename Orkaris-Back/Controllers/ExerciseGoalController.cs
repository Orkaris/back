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
    public class ExerciseGoalController : ControllerBase
    {
        private readonly IDataRepository<ExerciseGoal> dataRepository;
        private readonly IMapper _mapper;

        public ExerciseGoalController(IDataRepository<ExerciseGoal> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }
        
        //[Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExerciseGoalDTO>> GetExerciseGoalById(Guid id)
        {
            var exercise = await dataRepository.GetByIdAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return _mapper.Map<ExerciseGoalDTO>(exercise.Value);
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExerciseGoalDTO>> PostExerciseGoal(PostExerciseGoalDTO exerciseDTO)
        {
            var exercise = _mapper.Map<ExerciseGoal>(exerciseDTO);
            await dataRepository.AddAsync(exercise);

            return CreatedAtAction(nameof(GetExerciseGoalById), new { id = exercise.Id}, _mapper.Map<ExerciseGoalDTO>(exercise));
        }

        
        //[Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExerciseGoal(Guid id)
        {
            var exercise = await dataRepository.GetByIdAsync(id);
            if (exercise.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(exercise.Value!);

            return NoContent();
        }

        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutExerciseGoal(Guid id, PostExerciseGoalDTO exerciseDTO)
        {
            var existingExerciseGoal = await dataRepository.GetByIdAsync(id);
            if (existingExerciseGoal.Value == null)
            {
                return NotFound();
            }

            var exercise = _mapper.Map(exerciseDTO, existingExerciseGoal.Value);
            await dataRepository.UpdateAsync(existingExerciseGoal.Value, exercise);

            return NoContent();
        }


        
    }
    
}
