using AutoMapper;
using AutoMapper.Internal.Mappers;
using Azure;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Services;
using CarParkSystem.Service.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        // Kullanıcı oluşturma
        public async Task<CustomResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new AppUser { Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password); // Password'u hashleyip kaydeder

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return CustomResponseDto<AppUserDto>.Fail(400, errors);
            }

            return CustomResponseDto<AppUserDto>.Success(200, _mapper.Map<AppUserDto>(user));
        }

        // Kullanıcı bilgilerini getirme
        public async Task<CustomResponseDto<AppUserDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new NotFoundException("UserName not found");
            }

            return CustomResponseDto<AppUserDto>.Success(200, _mapper.Map<AppUserDto>(user));
        }
    }
}
