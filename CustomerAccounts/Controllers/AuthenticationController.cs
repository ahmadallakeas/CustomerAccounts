using Application.DataTransfer;
using Application.DataTransfer.Response;
using Application.Interfaces.IServices;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    [Produces("application/json")]

    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger _logger;

        public AuthenticationController(IServiceManager serviceManager, ILogger<AuthenticationController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }
        /// <summary>
        /// Creates a customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/authenticate/register
        ///     {        
        ///       "firstName": "Ahmad",
        ///       "lastName": "Al Lakeas",
        ///       "email": "ahmad.allakeas@gmail.com",
        ///       "password":"testpassword"
        ///     }
        /// </remarks>
        /// <param name="customerForRegistration"></param>
        /// <returns>A newly created customer</returns>
        /// <response code="201">Returns the login response</response>
        /// <response code="400">If the item is null</response>   
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
            LoginResponse response = new LoginResponse()
            {
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = await _serviceManager.AuthenticationService.CreateTokenAsync(),
                ExpiresIn = 60 * 60
            };
            return CreatedAtAction(nameof(Authenticate), new { customerForLogin = new { Email = customerForRegistration.Email, Password = customerForRegistration.Password } }, response);

        }
        /// <summary>
        /// Log a customer in
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/authenticate/login
        ///     {        
        ///     
        ///       "email": "ahmad.allakeas@gmail.com",
        ///       "password":"testpassword"
        ///     }
        /// </remarks>
        /// <param name="customerForLogin"></param>
        /// <returns>A newly created customer</returns>
        /// <response code="200">Returns login response</response>
        /// <response code="401">If credentials are incorrect</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
