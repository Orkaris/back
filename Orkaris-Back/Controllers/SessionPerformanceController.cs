using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Models.DTO;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;

namespace Orkaris_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionPerformanceController : ControllerBase
    {
        private readonly IDataRepository<SessionPerformance> dataRepository;
        private readonly IMapper _mapper;

        public SessionPerformanceController(IDataRepository<SessionPerformance> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SessionPerformanceDTO>> GetSessionPerformanceById(Guid id)
        {
            var sessionPerformance = await dataRepository.GetByIdAsync(id);

            if (sessionPerformance == null)
            {
                return NotFound();
            }

            return _mapper.Map<SessionPerformanceDTO>(sessionPerformance.Value);
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SessionPerformanceDTO>> PostSessionPerformance(PostSessionPerformanceDTO sessionPerformanceDTO)
        {
            var sessionPerformance = _mapper.Map<SessionPerformance>(sessionPerformanceDTO);
            await dataRepository.AddAsync(sessionPerformance);

            return CreatedAtAction(nameof(GetSessionPerformanceById), new { id = sessionPerformance.Id }, _mapper.Map<SessionPerformanceDTO>(sessionPerformance));
        }


        //[Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSessionPerformance(Guid id)
        {
            var sessionPerformance = await dataRepository.GetByIdAsync(id);
            if (sessionPerformance.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(sessionPerformance.Value!);

            return NoContent();
        }

        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSessionPerformance(Guid id, PostSessionPerformanceDTO sessionPerformanceDTO)
        {
            var existingSessionPerformance = await dataRepository.GetByIdAsync(id);
            if (existingSessionPerformance.Value == null)
            {
                return NotFound();
            }

            var sessionPerformance = _mapper.Map(sessionPerformanceDTO, existingSessionPerformance.Value);
            await dataRepository.UpdateAsync(existingSessionPerformance.Value, sessionPerformance);

            return NoContent();
        }
    }
}
