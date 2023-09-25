using CarParkSystem.Core.Configuration;
using CarParkSystem.Core.DTOs.Auth;
using CarParkSystem.Core.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Services.Auth
{
    public interface ITokenService
    {
        Task<TokenDto> CreateTokenAsync(AppUser userApp);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
