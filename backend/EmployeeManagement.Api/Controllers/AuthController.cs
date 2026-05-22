using EmployeeManagement.Application.Auth.Dtos;
using EmployeeManagement.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(
            [FromBody] RegisterRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _authService.RegisterAsync(
                    request,
                    cancellationToken);

                return Ok(response);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _authService.LoginAsync(
                    request,
                    cancellationToken);

                return Ok(response);
            }
            catch (UnauthorizedAccessException exception)
            {
                return Unauthorized(new { message = exception.Message });
            }
        }
    }
}
