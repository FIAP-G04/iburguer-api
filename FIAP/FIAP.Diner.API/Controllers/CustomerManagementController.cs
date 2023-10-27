using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.CustomerManagement.Identification;
using FIAP.Diner.Application.CustomerManagement.Registration;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerManagementController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public CustomerManagementController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerCommand command)
        {
            await _commandDispatcher.Dispatch(command, default);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerRegistrationInformationCommand command)
        {
            await _commandDispatcher.Dispatch(command, default);
            return Ok();
        }

        [HttpGet]
        [Route("{cpf}")]
        public async Task<IActionResult> IdentifyCustomer(string cpf)
        {
            var query = new IdentifyCustomerQuery(cpf);
            var result = await _queryDispatcher.Dispatch<IdentifyCustomerQuery, IdentifiedCustomer>(query, default);
            return Ok(result);
        }
    }
}
