using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Domain.Services.Interface;

namespace TestWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]

    public class AreasController : ControllerBase
    {
        private readonly IWorkAreaService _workAreaService;

        public AreasController(
             IWorkAreaService workAreaService)
        {
            _workAreaService = workAreaService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] AreaRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _workAreaService.Create(request));

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
                return Ok(await _workAreaService.FindAll());

            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

        [HttpGet("FindById")]
        public async Task<ActionResult> FindById([FromQuery] IdRequest id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _workAreaService.FindAll());

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
                return Ok(await _workAreaService.Delete(request.Id));

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
        public async Task<ActionResult> Update([FromBody] AreaRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _workAreaService.Update(request));

            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

    }
}
