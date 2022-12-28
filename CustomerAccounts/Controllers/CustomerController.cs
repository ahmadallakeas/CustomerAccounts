using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Application.Queries.CustomerQueries;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [Authorize]
    public class CustomerController : ControllerBase
    {

        private readonly IServiceManager _serviceManager;
        private ISender _sender;


        public CustomerController(IServiceManager serviceManager, ISender sender)
        {
            _serviceManager = serviceManager;
            _sender = sender;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(string id)
        {
            if (id.Length != 24)
            {
                Log.Error($"Customer with id {id} already exists");
                throw new CustomerNotFoundException("id", id);
            }
            var customer = await _sender.Send(new GetCustomerQuery(id, trackChanges: false));
            return Ok(customer);
        }

    }
}
