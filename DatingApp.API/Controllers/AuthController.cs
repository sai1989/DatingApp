using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistration userForRegistration)
        {
            if(await _repo.UserExists(userForRegistration.UserName.ToLower()))
                return BadRequest("User name already exists");
            var curUser=new User
            {
                UserName=userForRegistration.UserName.ToLower()
            };
            await _repo.Register(curUser,userForRegistration.Password);
            return StatusCode(201);
        }
    }
}