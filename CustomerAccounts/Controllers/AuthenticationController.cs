using Application.DataTransfer;
using Application.DataTransfer.Response;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger _logger;

        public AuthenticationController(IServiceManager serviceManager, ILogger<AuthenticationController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateCustomerAccount([FromBody] CustomerForRegistrationDto customerForRegistration)
        {
            var result = await _serviceManager.AuthenticationService.RegisterCustomerAsync(customerForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors.Where(s => s.Code != "DuplicateUserName"))
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            var user = await _serviceManager.AuthenticationService.GetAuthenticationUserAsync(customerForRegistration.Email);
            var customer = await _serviceManager.CustomerService.CreateCustomerAsync(customerForRegistration, user.Id, trackChanges: false);
            //LoginResponse response = new LoginResponse()
            //{
            //    CustomerId = customer.CustomerId,
            //    Email = customer.Email,
            //    FirstName = customer.FirstName,
            //    LastName = customer.LastName,
            //    Token = await _serviceManager.AuthenticationService.CreateTokenAsync(),
            //    ExpiresIn = 60 * 60
            //};
            return await Authenticate(new CustomerForLoginDto() { Email = customerForRegistration.Email, Password = customerForRegistration.Password });

        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] CustomerForLoginDto customerForLogin)
        {

            if (!await _serviceManager.AuthenticationService.ValidateCustomerAsync(customerForLogin))
            {
                return Unauthorized();
            }
            var user = await _serviceManager.AuthenticationService.GetAuthenticationUserAsync(customerForLogin.Email);
            var customer = await _serviceManager.CustomerService.GetCustomerByLoginAsync(user.Id, trackChanges: false);
            LoginResponse response = new LoginResponse()
            {
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = await _serviceManager.AuthenticationService.CreateTokenAsync(),
                ExpiresIn = 60 * 60
            };
            return Ok(response);

        }
    }
}
