using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Models.DTO;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;
using Orkaris_Back.Services;

namespace Orkaris_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IDataRepositoryUser dataRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public UsersController(IDataRepositoryUser dataRepository, IMapper mapper, JwtService jwtService)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }


        [Authorize]
        [HttpGet("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MinimalUserDTO>> GetUser(Guid id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || Guid.Parse(userIdFromToken) != id)
            {
                return Forbid();
            }

            var user = await dataRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<MinimalUserDTO>(user.Value);
        }



        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await dataRepository.GetByStringAsync(request.Email);

            if (user.Value == null)
            {
                return Unauthorized("User not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Value?.Password))
            {
                return Unauthorized("Invalid credentials");
            }


            var token = _jwtService.GenerateToken(user.Value!);

            return Ok(new { response = "success", token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterUserDTO request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await dataRepository.GetByStringAsync(request.Email);
            if (existingUser.Value != null)
            {
                return BadRequest("User already exists");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };

            await dataRepository.AddAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<MinimalUserDTO>(user));
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUser(Guid id, PutUserDTO userDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await dataRepository.GetByIdAsync(id);
            if (existingUser.Value == null)
            {
                return NotFound();
            }

            var user = _mapper.Map(userDTO, existingUser.Value);
            await dataRepository.UpdateAsync(existingUser.Value, user);

            return NoContent();
        }
    }
}