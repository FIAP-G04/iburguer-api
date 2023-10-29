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
    public async Task<IActionResult> RegisterCustomer(RegisterCustomerDTO dto)
    {
        await _account.RegisterCustomer(dto, default);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomerRegistrationInformation(
        UpdateCustomerRegistrationInformationDTO dto)
    {
        await _account.UpdateCustomerRegistrationInformation(dto, default);
        return Ok();
    }

    [HttpGet]
    [Route("{cpf}")]
    public async Task<IActionResult> IdentifyCustomer(string cpf)
    {
        var customer = await _identifier.Indentify(cpf, default);
        return Ok(customer);
    }
}