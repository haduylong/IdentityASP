using Identity.BLL.Services;
using Identity.Common.DTOs.Request;
using Identity.Common.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        //[Authorize(Roles = "ADMIN")]
        //[Authorize(Policy = "AdminOrUser")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUser()
        {
            var responses = await _userService.GetAllUserAsync();
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (response == null) 
            { 
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser(UserCreateRequest request)
        {
            var response = await _userService.CreateUserAsync(request);
            return CreatedAtAction(nameof(CreateUser), response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> UpdateUser(Guid id, UserUpdateRequest request)
        {
            var response = await _userService.UpdateUserAsync(id, request);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok(new
            {
                message = "Xoa user thanh cong"
            });
        }

        [Authorize]
        [HttpGet("myinfo")]
        public async Task<ActionResult<UserResponse>> GetMyInfo()
        {
            var userId = User.FindFirst("userId").Value;
            var response = await _userService.GetUserByIdAsync(Guid.Parse(userId));
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
