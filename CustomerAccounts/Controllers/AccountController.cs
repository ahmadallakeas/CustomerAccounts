using Application.DataTransfer.RequestParams;
using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/accounts")]
    [Authorize]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger _logger;
        public AccountController(IServiceManager serviceManager, ILogger<AccountController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccount(string id)
        {
            if (id.Length != 24)
            {
                throw new AccountNotFoundException(id);
            }
            var account = await _serviceManager.AccountService.GetAccountAsync(id, trackChanges: false);
            return Ok(account);

        }
        [HttpGet(Name = "GetAccounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccounts(string customerId)
        {
            if (customerId.Length != 24)
            {
                throw new CustomerNotFoundException("id", customerId);
            }
            var accounts = await _serviceManager.AccountService.GetAccountsAsync(customerId, trackChanges: false);
            return Ok(accounts);

        }
        [HttpGet("{accountId}/userInfo", Name = "GetUserInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserInfoAsync(string customerId, string accountId)
        {
            if (customerId.Length != 24)
            {
                throw new CustomerNotFoundException("id", customerId);
            }
            if (accountId.Length != 24)
            {
                throw new AccountNotFoundException(accountId);
            }
            var info = await _serviceManager.AccountService.GetUserInfoAsync(customerId, accountId, trackChanges: false);
            return Ok(info);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount(string customerId, [FromQuery] double initialCredits)
        {
            if (customerId.Length != 24)
            {
                throw new CustomerNotFoundException("id", customerId);
            }
            var account = await _serviceManager.AccountService.CreateAccountForCustomer(customerId, initialCredits, trackChanges: false);

            if (initialCredits > 0)
            {
                await _serviceManager.TransactionService.SendTransactionForAccount(account.AccountId, trackChanges: false);
            }
            account = await _serviceManager.AccountService.GetAccountForCustomerAsync(customerId, account.AccountId, trackChanges: false);
            return Ok(account);

        }
    }
}
