using Buissnes.Abstract;
using Entitites.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
         
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var login = await _authService.Login(loginDto);
            if (login.Success)
            {
                return Ok(login);
            }
            return BadRequest();

        }

    }
}
