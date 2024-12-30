using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Identity.BLL.Services;
using Identity.Common.DTOs.Response;
using Identity.Common.DTOs.Request;
using Microsoft.AspNetCore.Authorization;

namespace Identity.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _permissionService;

        public PermissionController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionResponse>>> GetAllPermission()
        {
            var responses = await _permissionService.GetAllPermissionAsync();
            return Ok(responses);
        }

        [HttpPost]
        public async Task<ActionResult<PermissionResponse>> CreatePermission(PermissionRequest request)
        {
            var response = await _permissionService.CreatePermissionAsync(request);
            return CreatedAtAction(nameof(CreatePermission), response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(string id)
        {
            await _permissionService.DeletePermissionAsync(id);
            return Ok(new
            {
                message = $"Xoá permission thành công",
            });
        }
    }
}
