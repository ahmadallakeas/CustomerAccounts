using Application.Commands.AccountCommands;
using Application.Commands.TransactionCommands;
using Application.DataTransfer.RequestParams;
using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Queries.AccountQueries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/accounts")]
    //    [Authorize]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;
        public AccountController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccount(string customerId, string id)
        {
            if (id.Length != 24)
            {
                Log.Error($"Account with id {id} does not exist");
                throw new AccountNotFoundException(id);
            }
            var account = await _sender.Send(new GetAccountForCustomerQuery(customerId, id, false));
            return Ok(account);

        }
        [HttpGet(Name = "GetAccounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccounts(string customerId)
        {
            if (customerId.Length != 24)
            {
                Log.Error($"Customer with id {customerId} does not exist");
                throw new CustomerNotFoundException("id", customerId);
            }
            var accounts = await _sender.Send(new GetAccountsQuery(customerId, false));
            return Ok(accounts);

        }
        [HttpGet("{accountId}/userInfo", Name = "GetUserInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserInfoAsync(string customerId, string accountId)
        {
            if (customerId.Length != 24)
            {
                Log.Error($"Customer with id {customerId} does not exist");
                throw new CustomerNotFoundException("id", customerId);
            }
            if (accountId.Length != 24)
            {
                Log.Error($"Account with id {accountId} does not exist");
                throw new AccountNotFoundException(accountId);
            }
            var info = await _sender.Send(new GetUserInfoQuery(customerId, accountId, false));
            return Ok(info);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount(string customerId, [Required, FromQuery] double initialCredits)
        {
            if (initialCredits < 0)
            {
                throw new CreateAccountBadRequestException(initialCredits);
            }
            if (customerId.Length != 24)
            {
                Log.Error($"Customer with id {customerId} does not exist");
                throw new CustomerNotFoundException("id", customerId);
            }
            var account = await _sender.Send(new CreateAccountForCustomerCommand(customerId, initialCredits, trackChanges: false));

            if (initialCredits > 0)
            {
                await _sender.Send(new SendTransactionForAccountCommand(account.AccountId, initialCredits, false));
            }
            account = await _sender.Send(new GetAccountForCustomerQuery(customerId, account.AccountId, false));
            return Ok(account);

        }
        [HttpPost("{accountId}/transaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MakeTransaction(string customerId, string accountId, [Required, FromQuery] double balance)
        {

            if (customerId.Length != 24)
            {
                Log.Error($"Customer with id {customerId} does not exist");
                throw new CustomerNotFoundException("id", customerId);
            }
            if (accountId.Length != 24)
            {
                Log.Error($"Account with id {accountId} does not exist");
                throw new AccountNotFoundException(accountId);
            }

            await _sender.Send(new SendTransactionForAccountCommand(accountId, balance: balance, false));
            return Ok();
        }
    }
}
