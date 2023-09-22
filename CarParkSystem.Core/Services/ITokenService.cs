﻿using CarParkSystem.Core.Configuration;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser userApp);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
