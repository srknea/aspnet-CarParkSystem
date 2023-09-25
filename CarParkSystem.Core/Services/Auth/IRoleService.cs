using CarParkSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Services.Auth
{
    public interface IRoleService
    {
        Task<CustomResponseDto<NoContentDto>> CreateUserRoles(string userName);
    }
}
