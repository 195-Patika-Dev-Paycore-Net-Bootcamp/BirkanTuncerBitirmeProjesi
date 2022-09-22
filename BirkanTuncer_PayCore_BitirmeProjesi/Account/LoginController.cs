using Microsoft.AspNetCore.Mvc;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using BirkanTuncer_PayCore_BitirmeProjesi.Service;
using Hangfire;

namespace BirkanTuncer_PayCore_BitirmeProjesi
{
    [ApiController]
    [Route("api/nhb/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public LoginController(ITokenService tokenService) // Standard login with jwt token. The token duration is set at 10 minutes.
        {
            this.tokenService = tokenService;
        }

        [HttpPost("Login")]
        public BaseResponse<TokenResponse> Login([FromBody] TokenRequest request)
        {
            var response = tokenService.GenerateToken(request);
            BackgroundJob.Enqueue(() => JobFireAndForget.Login(request.Email)); // A message is sent to the user when logged in.
            return response;
        }


    }
}
