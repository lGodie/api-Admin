using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TestWork.Common.Models;
using TestWork.Common.Requests;
using TestWork.Domain.Services.Interface;

namespace TestWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(
             IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _accountService.CreateUser(request));

            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] TokenRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _accountService.Login(request));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
