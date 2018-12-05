using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistrationDto userForRegistration)
        {
            if (await _repo.UserExists(userForRegistration.UserName.ToLower()))
                return BadRequest("User name already exists");
            var curUser = new User
            {
                UserName = userForRegistration.UserName.ToLower()
            };
            await _repo.Register(curUser, userForRegistration.Password);
            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
                //throw new Exception("I am so stupid");
            var userFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);
            if (userFromRepo == null)
                return Unauthorized();
            var claims= new []
            {
                //System.Security.Claim
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName)
            };
            //Microsoft.IdentityModel.Tokens
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature); 
             var tokenDescriptor= new SecurityTokenDescriptor()
             {
                 Subject=new ClaimsIdentity(claims),
                 Expires=System.DateTime.Now.AddDays(1),
                 SigningCredentials = creds
             };
            //System.IdentityModel.Tokens.Jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return Ok(new {token=tokenHandler.WriteToken(token)});    
        }
    }
}