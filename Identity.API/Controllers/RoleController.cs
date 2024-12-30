using Identity.BLL.Services;
using Identity.Common.DTOs.Request;
using Identity.Common.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Identity.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAllRole()
        {
            var responses = await _roleService.GetAllRoleAsync();
            return Ok(responses);
        }

        [HttpPost]
        public async Task<ActionResult<RoleResponse>> CreateRole(RoleRequest request)
        {
            var responses = await _roleService.CreateRoleAsync(request);
            return CreatedAtAction(nameof(CreateRole), responses);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            await _roleService.DeleteRoleAsync(id);
            return Ok(new
            {
                message = $"Role {id} deleted",
            });
        }
    }
}
