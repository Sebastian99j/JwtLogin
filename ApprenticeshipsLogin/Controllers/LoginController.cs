using ApprenticeshipsLogin.Models;
using ApprenticeshipsLogin.Models.Commands;
using ApprenticeshipsLogin.Models.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApprenticeshipsLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IMediator _mediator;

        public LoginController(ILogger<LoginController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            Console.WriteLine($"--- {command.Email}");
            string token = await _mediator.Send(command);

            HttpContext.Response.Cookies.Append(
                     "token", token,
                     new CookieOptions() { HttpOnly = true });

            return Ok(new TokenModel()
            {
                Token = token
            });
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            Console.WriteLine("done");
            var query = new GetAllUsersQuery();

            return Ok(await _mediator.Send(query));
        }
    }
}
