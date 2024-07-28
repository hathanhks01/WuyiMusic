using Microsoft.AspNetCore.Mvc;
using WuyiDAL.IReponsitory;
using WuyiDAL.Models;
using WuyiDAL.Models.systems;
using WuyiDAL.Repository;
using WuyiServices.IServices;
using WuyiServices.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WuyiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateRepo _authService;

        public AuthenticateController(IAuthenticateRepo authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistration userRegistration)
        {
            try
            {
                var token = await _authService.Register(userRegistration);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            try
            {
                var token = await _authService.Login(userLogin);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

