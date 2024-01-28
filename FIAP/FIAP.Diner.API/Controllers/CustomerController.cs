using FIAP.Diner.Application.Abstractions;
using FIAP.Diner.Application.Customers.Identification;
using FIAP.Diner.Application.Customers.Registration;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Diner.API.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IRegisterCustomerUseCase _registerCustomerUseCase;
    private readonly IUpdateCustomerRegistrationInformationUseCase _updateCustomerRegistrationInformationUseCase;
    private readonly IIdentifyCustomerUseCase _identifier;

    public CustomerController(IIdentifyCustomerUseCase identifier, IRegisterCustomerUseCase registerCustomerUseCase, IUpdateCustomerRegistrationInformationUseCase updateCustomerRegistrationInformationUseCase)
    {
        _identifier = identifier;
        _registerCustomerUseCase = registerCustomerUseCase;
        _updateCustomerRegistrationInformationUseCase = updateCustomerRegistrationInformationUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCustomer(RegisterCustomerDTO dto, CancellationToken cancellation)
    {
        await _registerCustomerUseCase.RegisterCustomer(dto, cancellation);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomerRegistrationInformation(
        UpdateCustomerRegistrationInformationDTO dto, CancellationToken cancellation)
    {
        await _updateCustomerRegistrationInformationUseCase.UpdateCustomerRegistrationInformation(dto, cancellation);
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