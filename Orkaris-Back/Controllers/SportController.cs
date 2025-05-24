using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Models.DTO;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly IDataRepository<Sport> dataRepository;
        private readonly IMapper _mapper;

        public SportController(IDataRepository<Sport> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SportDTO>> GetSportById(Guid id)
        {
            var sport = await dataRepository.GetByIdAsync(id);

            if (sport == null)
            {
                return NotFound();
            }

            return _mapper.Map<SportDTO>(sport.Value);
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SportDTO>> PostSport(PostSportDTO sportDTO)
        {
            var sport = _mapper.Map<Sport>(sportDTO);
            await dataRepository.AddAsync(sport);

            return CreatedAtAction(nameof(GetSportById), new { id = sport.Id }, _mapper.Map<SportDTO>(sport));
        }


        //[Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSport(Guid id)
        {
            var sport = await dataRepository.GetByIdAsync(id);
            if (sport.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(sport.Value!);

            return NoContent();
        }

        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSport(Guid id, PostSportDTO sportDTO)
        {
            var existingSport = await dataRepository.GetByIdAsync(id);
            if (existingSport.Value == null)
            {
                return NotFound();
            }

            var sport = _mapper.Map(sportDTO, existingSport.Value);
            await dataRepository.UpdateAsync(existingSport.Value, sport);

            return NoContent();
        }
    }
}
