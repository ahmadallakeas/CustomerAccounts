using Application.Commands.AuthenticationCommands;
using Application.DataTransfer;
using Application.DataTransfer.Response;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Application.Queries.CustomerQueries;
using AutoMapper;
using Azure;
using Domain.Entities;
using MediatR;
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
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public AuthenticationController(IServiceManager serviceManager, IMapper mapper, ISender sender)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _sender = sender;
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
            var result = await _sender.Send(new RegisterCustomerCommand(customerForRegistration));
            var customer = await _sender.Send(new GetCustomerByLoginCommand(customerForRegistration.Email, trackChanges: false));

            LoginResponse response = new LoginResponse()
            {
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = await _serviceManager.AuthenticationService.CreateTokenAsync(customer.CustomerId),
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
            var result = await _sender.Send(new ValidateCustomerCommand(customerForLogin));
            if (!result)
            {
                return Unauthorized();
            }
            var customer = await _sender.Send(new GetCustomerByLoginCommand(customerForLogin.Email, trackChanges: false));
            LoginResponse response = new LoginResponse()
            {
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = await _serviceManager.AuthenticationService.CreateTokenAsync(customer.CustomerId),
                ExpiresIn = 60 * 60
            };
            return Ok(response);

        }
    }
}
