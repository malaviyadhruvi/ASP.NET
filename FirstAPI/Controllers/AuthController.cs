
using FirstAPI.Dto;
using FirstAPI.GenericResponce;
using FirstAPI.IService;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDto userdto)
        {
            try
            {
                var result = await _authService.LoginUser(userdto);

                if (result.Item1 == 0)
                {
                    return NotFound(ResponseResult<string>.Failure(null, "Invalid email or password"));
                }

                if (result.Item1 == 1)
                {
                    return Ok(
                        ResponseResult<string>.Success(result.Item2, "Login successful"));
                }

                return BadRequest(ResponseResult<string>.Failure(null, result.Item2));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseResult<string>.Failure(null, "Something went wrong"));
                    
            }
        }
    }
}
