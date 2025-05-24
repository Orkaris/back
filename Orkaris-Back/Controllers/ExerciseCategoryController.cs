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
    public class ExerciseCategoryController : ControllerBase
    {
        private readonly IDataRepositoryInterTable<ExerciseCategory> dataRepository;
        private readonly IMapper _mapper;

        public ExerciseCategoryController(IDataRepositoryInterTable<ExerciseCategory> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet("{exerciseId}/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExerciseCategoryDTO>> GetExerciseCategoryByIds(Guid exerciseId, Guid categoryId)
        {
            var exerciseCategory = await dataRepository.GetByIds(exerciseId, categoryId);

            if (exerciseCategory == null)
            {
                return NotFound();
            }

            return _mapper.Map<ExerciseCategoryDTO>(exerciseCategory.Value);
        }
        //[Authorize]
        [HttpGet("ByExerciseId/{exerciseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExerciseCategoryDTO>> GetExerciseCategorysByExerciseId(Guid exerciseId, Guid categoryId)
        {
            var exerciseCategory = await dataRepository.GetByIds(exerciseId, categoryId);

            if (exerciseCategory == null)
            {
                return NotFound();
            }

            return _mapper.Map<ExerciseCategoryDTO>(exerciseCategory.Value);
        }



        //[Authorize]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExerciseCategoryDTO>> PostExerciseCategory(PostExerciseCategoryDTO exerciseCategoryDTO)
        {
            var exerciseCategory = _mapper.Map<ExerciseCategory>(exerciseCategoryDTO);
            await dataRepository.AddAsync(exerciseCategory);

            return CreatedAtAction(nameof(GetExerciseCategoryByIds), new { exerciseId = exerciseCategory.ExerciseId, categoryId = exerciseCategory.CategoryId }, _mapper.Map<ExerciseCategoryDTO>(exerciseCategory));
        }


        //[Authorize]
        [HttpDelete("{exerciseId}/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExerciseCategory(Guid exerciseId, Guid categoryId)
        {
            var exerciseCategory = await dataRepository.GetByIds(exerciseId, categoryId);
            if (exerciseCategory.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(exerciseCategory.Value!);

            return NoContent();
        }


        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutExerciseCategory(Guid id, PostExerciseCategoryDTO exerciseCategoryDTO)
        {
            var existingExerciseCategory = await dataRepository.GetByIdAsync(id);
            if (existingExerciseCategory.Value == null)
            {
                return NotFound();
            }

            var exerciseCategory = _mapper.Map(exerciseCategoryDTO, existingExerciseCategory.Value);
            await dataRepository.UpdateAsync(existingExerciseCategory.Value, exerciseCategory);

            return NoContent();
        }
    }
}
