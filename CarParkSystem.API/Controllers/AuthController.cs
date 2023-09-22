using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParkSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _customAuthenticationService;

        public AuthController(IAuthenticationService customAuthenticationService)
        {
            _customAuthenticationService = customAuthenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _customAuthenticationService.CreateTokenAsync(loginDto);

            /*
            if (result.StatusCode == 200)
            {
                return Ok();
            }
            else if(result.StatusCode == 404)
            {
                return NotFound();
            }
            */

            return CreateActionResult(result);
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result = _customAuthenticationService.CreateTokenByClient(clientLoginDto);

            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _customAuthenticationService.RevokeRefreshToken(refreshTokenDto.RefreshToken);

            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)

        {
            var result = await _customAuthenticationService.CreateTokenByRefreshToken(refreshTokenDto.RefreshToken);

            return CreateActionResult(result);
        }
    }

}
