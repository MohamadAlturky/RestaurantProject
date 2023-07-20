using Application.Customers.UseCases.DecreaseCustomerBalance;
using Application.Customers.UseCases.GetAll;
using Application.Customers.UseCases.GetSumOfCustomersBalances;
using Application.UseCases.Customers.Create;
using Application.UseCases.Customers.GetByFilter;
using Application.UseCases.Customers.GetPage;
using Application.UseCases.Customers.IncreaseCustomerBalance;
using Domain.Customers.Aggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels.Customers;
using Presentation.Mappers;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CustomersController : APIController
{
	private readonly ILogger<CustomersController> _logger;

	public CustomersController(ISender sender, IMapper mapper, ILogger<CustomersController> logger) : base(sender, mapper)
	{
		_logger = logger;
	}


	[HttpPost("Create")]
	public async Task<IActionResult> Create([FromBody] CustomerDTO customer)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
		}

		try
		{
			Result response = await _sender.Send(new CreateCustomerCommand(_mapper.Map(customer)));

			if (response.IsFailure)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
		catch (Exception exception)
		{
			return BadRequest(Result.Failure(new Error("Model State", exception.Message)));
		}
	}


	[HttpGet("GetAllCustomers")]
	public async Task<IActionResult> GetAllCustomers()
	{
		Result<List<Customer>> response = await _sender.Send(new GetAllCustomersQuery());

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response.Value.Select(customer=>_mapper.Map(customer)).ToList());
	}

	[HttpGet("GetPaginatedCustomers")]
	public async Task<IActionResult> GetPaginatedCustomers(int pageNumber, int pageSize)
	{
		Result<List<Customer>> response = await _sender.Send(new GetCustomersPageQuery(pageSize, pageNumber));

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response.Value.Select(customer => _mapper.Map(customer)).ToList());
	}

	[HttpGet("GetCustomerBySerialNumber")]
	public async Task<IActionResult> GetCustomerBySerialNumber(int serialNumber)
	{
		Result<Customer> response = await _sender.Send(new GetCustomerBySerialNumberQuery(serialNumber));

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(_mapper.Map(response.Value));
	}

	[HttpGet("GetSumOfCustomersBalances")]
	public async Task<IActionResult> GetSumOfCustomersBalances()
	{
		Result<long> response = await _sender.Send(new GetSumOfCustomersBalancesQuery());

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response.Value);
	}

	[HttpPut("IncreaseCustomerBalance")]
	public async Task<IActionResult> IncreaseCustomerBalance(int serialNumber, int valueToAdd)
	{
		Result response = await _sender.Send(new IncreaseCustomerBalanceCommand(serialNumber, valueToAdd));

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response);
	}

	[HttpPut("DecreaseCustomerBalance")]
	public async Task<IActionResult> DecreaseCustomerBalance(int serialNumber, int valueToRemove)
	{
		Result response = await _sender.Send(new DecreaseCustomerBalanceCommand(serialNumber, valueToRemove));

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response);
	}
}
