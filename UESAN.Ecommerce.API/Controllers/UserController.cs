using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UESAN.Ecommerce.CORE.Core.DTOs;
using UESAN.Ecommerce.CORE.Core.Interfaces;

namespace UESAN.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO request)
        {
            if (request == null) return BadRequest();
            var user = await _userService.SignIn(request.Email ?? string.Empty, request.Password ?? string.Empty);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserCreateDTO userCreateDTO)
        {
            if (userCreateDTO == null) return BadRequest();
            var newUserId = await _userService.SignUp(userCreateDTO);
            return CreatedAtAction(nameof(SignUp), new { id = newUserId }, new { Id = newUserId });
        }
    }
}
