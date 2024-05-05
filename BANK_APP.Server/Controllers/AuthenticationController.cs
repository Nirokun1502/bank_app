using BANK_APP.Server.DTOs;
using BANK_APP.Server.Interfaces;
using BANK_APP.Server.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BANK_APP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Account>> Register(AccountDTO request)
        {
           var account = await  _authenticationService.Register(request);

            if (account == null)
            {
                return BadRequest("Tài khoản hoặc mật khẩu không đúng định dạng, vai trò không tồn tại, hoặc tên tài khoản đã tồn tại");
            }              

            return Ok(account);
        }

        [HttpPost("changepassword"), Authorize]
        public async Task<ActionResult<string>> ChangePassword(  ChangePasswordRequestDTO request )
        {
            var result = await _authenticationService.ChangePassword(request);
            if (result == null)
            {
                return BadRequest("Mật khẩu cũ không đúng");
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login(AccountDTO request)
        {
            var account = await _authenticationService.Login(request);

            if (account == null)
            {
                return BadRequest("Wrong Username or Password");             
            }

            if (account == "1")
            {
                return BadRequest("Your account doesn't have permissions to login");
            }

            return Ok(account);
        }

        [HttpPost("logout"),Authorize]
        public async Task<ActionResult> Logout()
        {
          await _authenticationService.Logout();

            return Ok("Đăng xuất thành công");
        }

        [HttpPost("getaccountinfo"), Authorize]
        public async Task<ActionResult<object>> GetAccountInfo()
        {
          var result = await _authenticationService.GetAccountInfo();

            if (result is null)
            {
                return "No info, please login";
            }

            return Ok(result);
        }

    }
}
