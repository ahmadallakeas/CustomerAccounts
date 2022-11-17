using Application.DataTransfer.RequestParams;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/account")]
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
        [HttpGet("userInfo/{id:int}", Name = "GetUserInfo")]
        public async Task<IActionResult> GetUserInfoAsync(int id)
        {
            var info = await _serviceManager.AccountService.GetUserInfoAsync(id, trackChanges: false);
            return Ok(info);

        }
        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync(RequestBody requestBody)
        {
            var account = await _serviceManager.AccountService.CreateAccountForCustomer(requestBody, trackChanges: false);

            if (requestBody.initialCredit > 0)
            {
                await _serviceManager.TransactionService.SendTransactionForAccount(account.AccountId, trackChanges: false);
            }
            account = await _serviceManager.AccountService.GetAccountAsync(account.AccountId, trackChanges: false);
            return CreatedAtRoute("GetAccount", new { id = account.AccountId }, account);

        }
    }
}
