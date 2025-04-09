using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using Orkaris_Back.Models.Repository;
using AutoMapper;
using Orkaris_Back.Services;
using Orkaris_Back.Models.EntityFramework;

namespace Orkaris_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IDataRepositoryString<User> dataRepositoryUser;
        private readonly IDataRepositoryString<EmailConfirmationToken> dataRepositoryEmail;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        private readonly EmailService _emailService;

        public EmailController(IDataRepositoryString<User> dataRepositoryUser, IMapper mapper, JwtService jwtService, IDataRepositoryString<EmailConfirmationToken> dataRepositoryEmail, EmailService emailService)
        {
            this.dataRepositoryEmail = dataRepositoryEmail;
            this.dataRepositoryUser = dataRepositoryUser;
            _mapper = mapper;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            var confirmationToken = await dataRepositoryEmail.GetByStringAsync(token);

            if (confirmationToken == null || confirmationToken.Value?.ExpirationDate < DateTime.UtcNow || confirmationToken.Value!.IsUsed)
            {
                return BadRequest("Lien de confirmation invalide ou expiré.");
            }

            var user = await dataRepositoryUser.GetByIdAsync(confirmationToken.Value.UserId);

            if (user == null)
            {
                return BadRequest("Utilisateur non trouvé.");
            }

            user.Value!.IsVerified = true;
            confirmationToken.Value.IsUsed = true;

            await dataRepositoryUser.UpdateAsync(user.Value, user.Value);
            await dataRepositoryEmail.UpdateAsync(confirmationToken.Value, confirmationToken.Value);

            return Ok("Email confirmé avec succès !");
        }


    }
}
