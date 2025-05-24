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
    public class CategoryController : ControllerBase
    {
        private readonly IDataRepository<Category> dataRepository;
        private readonly IMapper _mapper;

        public CategoryController(IDataRepository<Category> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(Guid id)
        {
            var category = await dataRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return _mapper.Map<CategoryDTO>(category.Value);
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDTO>> PostCategory(PostCategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await dataRepository.AddAsync(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, _mapper.Map<CategoryDTO>(category));
        }


        //[Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await dataRepository.GetByIdAsync(id);
            if (category.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(category.Value!);

            return NoContent();
        }

        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategory(Guid id, PostCategoryDTO categoryDTO)
        {
            var existingCategory = await dataRepository.GetByIdAsync(id);
            if (existingCategory.Value == null)
            {
                return NotFound();
            }

            var category = _mapper.Map(categoryDTO, existingCategory.Value);
            await dataRepository.UpdateAsync(existingCategory.Value, category);

            return NoContent();
        }
    }
}
