using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [Authorize]
    public class CustomerController : ControllerBase
    {

        private readonly IServiceManager _serviceManager;
        private readonly ILogger _logger;


        public CustomerController(IServiceManager serviceManager, ILogger<CustomerController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(string id)
        {
            if (id.Length != 24)
            {
                throw new CustomerNotFoundException("id", id);
            }
            var customer = await _serviceManager.CustomerService.GetCustomerAsync(id, trackChanges: false);
            return Ok(customer);
        }

    }
}
