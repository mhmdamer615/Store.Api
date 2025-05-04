﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
            => Ok(await serviceManager.AuthenticationService.LoginAsync(loginDto));
        [HttpPost]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
            => Ok(await serviceManager.AuthenticationService.RegisterAsync(registerDto));

        [HttpGet]
        public async Task<ActionResult<bool>> IsEmailExist(string email)
            => Ok(await serviceManager.AuthenticationService.IsEmailExist(email));



        [HttpGet]
        [Authorize]
        public async Task<ActionResult<bool>> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = Ok(await serviceManager.AuthenticationService.GetUserByEmailAsync(email));
            return result;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = Ok(await serviceManager.AuthenticationService.GetUserAddressAsync(email));
            return result;
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = Ok(await serviceManager.AuthenticationService.UpdateUserAddressAsync(email, addressDto));
            return result;
        }

    }
}
