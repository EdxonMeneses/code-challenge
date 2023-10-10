using DB.Contracts;
using DB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> GetTokenGenerated([FromBody] TokenGenerationRequest tokenGenerationRequest)
        {
            string jwtToken = _authService.BuildToken(tokenGenerationRequest);
            return this.Ok(jwtToken);
        }
    }
}
