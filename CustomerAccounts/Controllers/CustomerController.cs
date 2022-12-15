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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var company = await _serviceManager.CustomerService.GetCustomerAsync(id, trackChanges: false);
            return Ok(company);
        }

    }
}
