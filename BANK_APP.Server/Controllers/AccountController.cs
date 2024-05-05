using BANK_APP.Server.DTOs;
using BANK_APP.Server.Interfaces;
using BANK_APP.Server.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BANK_APP.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {

            _accountService = accountService; // đánh dấu rằng class này có thể xài DI của interface
        }

        [HttpGet(Name = "GetAccount"), Authorize]
        public async Task<ActionResult<List<Object>>> GetAllAccounts()
        {
            var result = await _accountService.GetAllAccounts();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            var result = await _accountService.GetAccountById(id);
            if (result == null)
            {
                return NotFound("Account not found");
            }

            return Ok(result);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Account>> AddAccount(AccountDTO account)
        {
            var result = await _accountService.AddAccount(account);
            return Ok(result);
        }

        [HttpPost("role"), Authorize]
        public async Task<ActionResult<Account>> AddAccountRole(Account_Roles request)
        {
            var result = await _accountService.AddAccountRole(request);
            if (result == null)
            {
                return NotFound("Account or Role not found");
            }
            return Ok(result);
        }

        [HttpDelete("role")]
        public async Task<ActionResult<Account>> RemoveAccountRole(Account_Roles request)
        {
            var result = await _accountService.RemoveAccountRole(request);
            if (result == null)
            {
                return NotFound("Account or Role not found");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Account>>> UpdateAccount(int id, Account request)
        {
           var result  = await _accountService.UpdateAccount(id, request);
           if(result == null)
            {
                return NotFound("Account not found");
            }
           return Ok(result);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<List<Account>>> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccount(id);
            if (result == null)
            {
                return NotFound("Account not found");
            }
            return Ok(result);
        }

    }
}
