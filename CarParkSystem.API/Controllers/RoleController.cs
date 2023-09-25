using CarParkSystem.Core.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParkSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : CustomBaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("[action]/{userName}")]
        public async Task<IActionResult> CreateUserRoles(string userName)
        {
            return CreateActionResult(await _roleService.CreateUserRoles(userName));
        }
    }
}
