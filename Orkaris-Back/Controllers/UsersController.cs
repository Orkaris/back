using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Orkaris_Back.Attribute;
using Orkaris_Back.Models.DataManager;
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

        private readonly IDataRepositoryString<User> dataRepository;
        private readonly IDataRepositoryString<EmailConfirmationToken> dataRepositoryEmail;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        private readonly EmailService _emailService;

        public UsersController(IDataRepositoryString<User> dataRepository, IMapper mapper, JwtService jwtService, IDataRepositoryString<EmailConfirmationToken> dataRepositoryEmail, EmailService emailService)
        {
            this.dataRepositoryEmail = dataRepositoryEmail;
            this.dataRepository = dataRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _emailService = emailService;
        }


        [Authorize]
        [AuthorizeUserMatch]
        [HttpGet("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InfoUserDTO>> GetUser(Guid id)
        {
            var user = await dataRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<InfoUserDTO>(user.Value);
        }



        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {

            var user = await dataRepository.GetByStringAsync(request.Email!);


            if (user.Value == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Value?.Password))
            {
                return Unauthorized("Invalid credentials");
            }
            if (!user.Value!.IsVerified)
            {
                return BadRequest("Email not verified");
            }


            var token = _jwtService.GenerateToken(user.Value!);

            return Ok(new { user.Value.Id, token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterUserDTO request)
        {

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


            var token = Guid.NewGuid().ToString();
            var emailConfirmationToken = new EmailConfirmationToken
            {
                Token = token,
                UserId = user.Id,
                ExpirationDate = DateTime.UtcNow.AddDays(1)
            };
            await dataRepositoryEmail.AddAsync(emailConfirmationToken);
            var confirmationLink = Url.Action(nameof(EmailController.VerifyEmail), "Email", new { token }, Request.Scheme);
            var emailContent = $@"
                <html>
                    <body>
                        <h1>Welcome to Orkaris!</h1>
                        <p>Thank you for registering. Please confirm your email address by clicking the link below:</p>
                        <a href='{confirmationLink}' style='color: #fff; background-color: #007bff; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Confirm Email</a>
                        <p>If you did not register for Orkaris, please ignore this email.</p>
                        <p>Best regards,<br/>The Orkaris Team</p>
                    </body>
                </html>";
            var emailSubject = "Email Confirmation";
            await _emailService.SendEmailAsync(user.Email, emailSubject, emailContent);

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

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(Guid id)
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
            var existingUser = await dataRepository.GetByIdAsync(id);
            if (existingUser.Value == null || existingUser.Value.Id != id)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(existingUser.Value);

            return NoContent();
        }
    }
}