using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Customers.Identification;
using FIAP.Diner.Application.Customers.Registration;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerAccount _account;
    private readonly ICustomerIdentifier _identifier;

    public CustomerController(ICustomerAccount account, ICustomerIdentifier identifier)
    {
        _account = account;
        _identifier = identifier;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCustomer(RegisterCustomerDTO dto, CancellationToken cancellation)
    {
        await _account.RegisterCustomer(dto, cancellation);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomerRegistrationInformation(
        UpdateCustomerRegistrationInformationDTO dto, CancellationToken cancellation)
    {
        await _account.UpdateCustomerRegistrationInformation(dto, cancellation);
        return Ok();
    }

    [HttpGet]
    [Route("{cpf}")]
    public async Task<IActionResult> IdentifyCustomer(string cpf, CancellationToken cancellation)
    {
        var customer = await _identifier.Indentify(cpf, cancellation);
        return Ok(customer);
    }
}