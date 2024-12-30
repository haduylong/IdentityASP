using Identity.BLL.Services;
using Identity.Common.DTOs;
using Identity.Common.DTOs.Request;
using Identity.Common.DTOs.Response;
using Identity.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("token")]
        public async Task<ActionResult<ApiResponse<AuthenticationResponse>>> Authenticate(AuthenticationRequest request)
        {
            var response = await _authenticationService.Authenticate(request);

            return Ok(new ApiResponse<AuthenticationResponse> {
                Message = "Authentication Successful",
                Result = response
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout(User);

            return Ok(new
            {
                message = "Logout successful!"
            });
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<AuthenticationResponse>>> RefreshToken(AuthenticationRequest request)
        {
            var response = await _authenticationService.RefreshToken(HttpContext);

            return Ok(new ApiResponse<AuthenticationResponse>
            {
                Message = "Refresh Token Successful",
                Result = response
            });
        }
    }
}
