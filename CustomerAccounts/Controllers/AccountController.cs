using Application.DataTransfer.RequestParams;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/accounts")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger _logger;
        public AccountController(IServiceManager serviceManager, ILogger<AccountController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }
        [HttpGet("{id:int}", Name = "GetAccount")]
        public async Task<IActionResult> GetAccountAsync(int id)
        {
            var account = await _serviceManager.AccountService.GetAccountAsync(id, trackChanges: false);
            return Ok(account);

        }
        [HttpGet(Name = "GetAccounts")]
        public async Task<IActionResult> GetAccountsAsync(int customerId)
        {
            var accounts = await _serviceManager.AccountService.GetAccountsAsync(customerId, trackChanges: false);
            return Ok(accounts);

        }
        [HttpGet("{id:int}/userInfo", Name = "GetUserInfo")]
        public async Task<IActionResult> GetUserInfoAsync(int id)
        {
            var info = await _serviceManager.AccountService.GetUserInfoAsync(id, trackChanges: false);
            return Ok(info);

        }
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccountAsync(int customerId, [FromQuery] double initialCredits)
        {
            var account = await _serviceManager.AccountService.CreateAccountForCustomer(customerId, initialCredits, trackChanges: false);

            if (initialCredits > 0)
            {
                await _serviceManager.TransactionService.SendTransactionForAccount(account.AccountId, trackChanges: false);
            }
            account = await _serviceManager.AccountService.GetAccountAsync(account.AccountId, trackChanges: false);
            return CreatedAtRoute("GetAccount", new { id = account.AccountId }, account);

        }
    }
}
