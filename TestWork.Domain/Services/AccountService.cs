using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestWork.Common.Models;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Domain.Services.Interface;
using TestWork.Infrastructure.Data.Entities;
using TestWork.Infrastructure.Data.Repositories.Interface;

namespace TestWork.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly TestRappiContext _context;
        private readonly IWorkSubAreaRepository _workSubAreaRepository;
        private readonly IIdentificationTypesRepository _iIdentificationTypesRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUsersRepository _usersRepository;

        public AccountService(
            IConfiguration configuration,
            TestRappiContext context,
            IWorkSubAreaRepository workSubAreaRepository,
            IUsersRepository usersRepository,
            IIdentificationTypesRepository identificationTypesRepository,
            IRoleRepository roleRepository
            )
        {
            _configuration = configuration;
            _context = context;
            _workSubAreaRepository = workSubAreaRepository;
            _iIdentificationTypesRepository = identificationTypesRepository;
            _usersRepository = usersRepository;
            _roleRepository = roleRepository;
        }
        public async Task<Response> CreateUser(UserRequest model)
        {
            var IdSubArea = await _workSubAreaRepository.FindById(model.IdWorkSubArea);
            var IdentificationType = await _iIdentificationTypesRepository.FindById(model.IdIdentificationTypes);
            var IdRole= await _roleRepository.FindById(model.IdRole);
            var user = new Users
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Document = model.Document,
                IdWorkSubArea = IdSubArea.Id,
                IdIdentificationType = IdentificationType.Id,
                IdRole = IdRole.Id,
                Username = model.Username,
                Password = model.Password,
            };

            int result= await _usersRepository.CreateUser(user);
        
            if (result == 1)
            {
                return new Response
                {
                    Result = BuildToken(model.Email, IdRole.Name),
                    IsSuccess = true,
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Username or password invalid"
                };
            }
        }

        public async Task<Response> Login(TokenRequest model)
        {
            var result = await _usersRepository.Login(model);
            if (result)
            {
                var user = await _usersRepository.FindByEmail(model.Email);
                return new Response
                {
                    Result = BuildToken(model.Email, user.Role),
                    IsSuccess = true,
                };

            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Invalid login attempt."
                };

            }
        }

        private UserToken BuildToken(string email, string rol)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                claims.Add(new Claim(ClaimTypes.Role, rol));
            

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(1);
            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials);
            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
