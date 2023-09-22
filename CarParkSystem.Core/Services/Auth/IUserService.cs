using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Services.Auth
{
    public interface IUserService
    {
        Task<CustomResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<CustomResponseDto<AppUserDto>> GetUserByNameAsync(string userName);
    }
}
