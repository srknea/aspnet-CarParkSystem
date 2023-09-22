using Azure;
using CarParkSystem.Core.Configuration;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.DTOs.Auth;
using CarParkSystem.Core.Model.Auth;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Core.Services.Auth;
using CarParkSystem.Core.UnitOfWork;
using CarParkSystem.Service.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Service.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager; // Identity User
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) throw new ClientSideException("Email or Password is wrong");


            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new ClientSideException("Email or Password is wrong");
            }
            var token = _tokenService.CreateToken(user); // Token oluşturuldu

            var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync(); // Kullanıcının refresh token'ı var mı? 

            // Kullanıcının refresh token'ı yoksa kendimiz oluşturacağız
            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration }); // Kullanıcıya refresh token oluşturduk
            }
            else
            {
                // Kullanıcının refresh tokenı zaten varsa, güncelleyeceğiz...
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDto>.Success(200, token); // Oluşturulan token döndürüldü
        }

        public CustomResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret); // Gelen client id ve client secret ile eşleşen bir client appsettings.json 'da var mı?

            if (client == null)
            {
                throw new NotFoundException("ClientId or ClientSecret not found");
            }

            var token = _tokenService.CreateTokenByClient(client); // Token oluşturuldu

            return CustomResponseDto<ClientTokenDto>.Success(200, token);
        }

        // Refresh token ile yeni bir token oluşturma
        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync(); // Refresh token var mı?

            if (existRefreshToken == null)
            {
                throw new NotFoundException("Refresh token not found");
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId); // Kullanıcı var mı?

            if (user == null)
            {
                throw new NotFoundException("User Id not found");
            }

            var tokenDto = _tokenService.CreateToken(user); // Token oluşturuldu

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDto>.Success(200, tokenDto); // Oluşturulan token döndürüldü
        }

        // Kullanıcı logout olduğunda refresh token'ı silecek olan mothot
        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync(); // Refresh token var mı?

            if (existRefreshToken == null)
            {
                throw new NotFoundException("Refresh token not found");
            }

            _userRefreshTokenService.Remove(existRefreshToken); // Refresh token silindi

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);
        }
    }
}
