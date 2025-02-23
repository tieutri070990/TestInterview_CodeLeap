using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebAPI_Structure.App.DTO;
using WebAPI_Structure.Infra.Services.UserAdmin;
using WebAPI_Structure.Core.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI_Structure.Infra.Context;
using Microsoft.AspNetCore.Authorization;
using ErrorOr;

namespace WebAPI_Structure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserDTO request)
        {
            var result = await _userServices.Login(request);
            if (result.IsError)
                return Ok(result.FirstError);

            return Ok(result.Value);        
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> Create([FromBody] UserInfoDTO request)
        {
            var result = await _userServices.Create(request);
            if (result.IsError)
                return Ok(result.FirstError);

            return Ok(result.Value);
        }

        [Authorize]
        [HttpPost("GetUserInfo")]
        public async Task<ActionResult> GetUserInfo(string email)
        {
            var result = await _userServices.GetUserInfo(email);
            if (result.IsError)
                return Ok(result.FirstError);

            return Ok(result.Value);
        }
     
    }
}
