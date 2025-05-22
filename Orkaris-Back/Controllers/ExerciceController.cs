using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Attribute;
using Orkaris_Back.Models.DTO;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciceController : ControllerBase
    {
        private readonly IDataRepository<Exercise> dataRepository;
        private readonly IMapper _mapper;

        public ExerciceController(IDataRepository<Exercise> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }
        
        [Authorize]
        [HttpGet("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExerciseDTO>> GetExerciseById(Guid id)
        {
            var exercise = await dataRepository.GetByIdAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return _mapper.Map<ExerciseDTO>(exercise.Value);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExerciseDTO>> PostExercise(PostExerciseDTO exerciseDTO)
        {
            var exercise = _mapper.Map<Exercise>(exerciseDTO);
            await dataRepository.AddAsync(exercise);

            return CreatedAtAction(nameof(GetExerciseById), new { id = exercise.Id}, _mapper.Map<ExerciseDTO>(exercise));
        }

        
        [Authorize]
        [HttpDelete("{id}/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var exercise = await dataRepository.GetByIdAsync(id);
            if (exercise.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(exercise.Value!);

            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutExercise(Guid id, PostExerciseDTO exerciseDTO)
        {
            var existingExercise = await dataRepository.GetByIdAsync(id);
            if (existingExercise.Value == null)
            {
                return NotFound();
            }

            var exercise = _mapper.Map(exerciseDTO, existingExercise.Value);
            await dataRepository.UpdateAsync(existingExercise.Value, exercise);

            return NoContent();
        }


        
    }
}
