using ApprenticeshipsLogin.Models.Commands;
using ApprenticeshipsLogin.Models.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            string token = await _mediator.Send(command);

            return Ok(token);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            return Ok(await _mediator.Send(query));
        }
    }
}
