using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWork.Common.Models;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Domain.Services.Interface;

namespace TestWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class usersController : ControllerBase
    {
        private readonly IUserService _userService;

        public usersController(
             IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> search([FromQuery] PaginationModel paginationModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _userService.search(paginationModel));

            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpGet("FindAll")]
        public async Task<ActionResult> FindAll()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _userService.FindAll());

            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

        
        [HttpPost("Delete")]
        public async Task<ActionResult> Delete([FromQuery] IdRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _userService.Delete(request.Id));

            }
            catch (Exception ex)
            {

                return BadRequest(new Response
                {
                    IsSuccess = true,
                    Message = ex.Message

                } );
            }

        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update([FromBody] UserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _userService.Update(request));

            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

    }
}
