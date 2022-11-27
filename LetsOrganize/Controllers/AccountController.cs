using LetsOrganize.Interfaces;
using LetsOrganize.Models;
using Microsoft.AspNetCore.Mvc;

namespace LetsOrganize.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Incorrect data entered");
            }

            await _accountService.RegisterUser(userDto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Incorrect data entered");
            }

            string token = await _accountService.GenerateJwtToken(dto);

            return Ok(token);
        }

    }
}
