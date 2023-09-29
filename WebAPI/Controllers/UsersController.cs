using Buissnes.Abstract;
using Entitites.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        public  async Task<IActionResult> GetList()
        {
            var result = await _userService.GetListAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        
        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] UserAddDto userAddDto)
        {
            var result = await _userService.AddAsync(userAddDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var result = await _userService.UpdateAsync(userUpdateDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

            
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (result)
            {
                return Ok(true);
            }
            return BadRequest(false);
        }


        [AllowAnonymous] // dışardan herkes gelebilir
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate([FromBody] UserForLoginDto userForLoginDto)
        {
            var result = await _userService.Authenticate(userForLoginDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
