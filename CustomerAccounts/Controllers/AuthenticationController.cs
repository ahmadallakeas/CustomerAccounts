using Application.DataTransfer;
using Application.DataTransfer.Response;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using AutoMapper;
using Azure;
using Domain.Entities;
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
        private readonly IMapper _mapper;


        public AuthenticationController(IServiceManager serviceManager, ILogger<AuthenticationController> logger, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _logger = logger;
            _mapper = mapper;
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
            var customer = await _serviceManager.AuthenticationService.GetCustomerByEmail(customerForRegistration.Email);
            LoginResponse response = new LoginResponse()
            {
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = await _serviceManager.AuthenticationService.CreateTokenAsync(),
                ExpiresIn = 60 * 60
            };
            var x = CreatedAtAction(nameof(Authenticate), new { customerForLogin = new { Email = customerForRegistration.Email, Password = customerForRegistration.Password } }, response);
            return x;
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
            var customer = await _serviceManager.CustomerService.GetCustomerByLoginAsync(customerForLogin.Email, trackChanges: false);
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
