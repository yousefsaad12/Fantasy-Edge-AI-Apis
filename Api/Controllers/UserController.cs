using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {   
        private readonly IAuthServices _authServices;
        private readonly ITokenServices _tokenService;
        public UserController(IAuthServices authServices, ITokenServices tokenServices)
        {
            _authServices = authServices;
            _tokenService = tokenServices;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
        {
            if(!ModelState.IsValid) return BadRequest(userRequest);

            if(await _authServices.CheckExist(userRequest.email).ConfigureAwait(false)) return BadRequest("This email already used");

            var results = await _authServices.Register(userRequest).ConfigureAwait(false);

            if(results.Succeeded) return Ok(new UserResponse { userName = userRequest.userName, email = userRequest.email, token =  _tokenService.CreateToken(userRequest) });

            return StatusCode(500, results.Errors);
        }


    }
}