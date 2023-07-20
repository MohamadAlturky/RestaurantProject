using Application.Pricing.UseCases.Create;
using Application.Pricing.UseCases.Delete;
using Application.Pricing.UseCases.GetAll;
using Application.Pricing.UseCases.Update;
using Domain.Shared.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels.PricingRecords;
using Presentation.Mappers;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PricingController : APIController
{
	private readonly ILogger<PricingController> _logger;

	public PricingController(ISender sender, IMapper mapper, ILogger<PricingController> logger) 
		: base(sender, mapper)
	{
		_logger = logger;
	}


	[HttpPost("Create")]
	public async Task<IActionResult> Create([FromBody] PricingRecordDTO recordDTO)
	{
		try
		{
			PricingRecord record = _mapper.Map(recordDTO);
			Result response = await _sender.Send(new CreatePricingRecordCommand(record));

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

	[HttpGet("GetAllPricingRecords")]
	public async Task<IActionResult> GetAllPricingRecords()
	{
		Result<List<PricingRecord>> response = await _sender.Send(new GetAllPricingRecordsQuery());

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response.Value.Select(record => _mapper.Map(record)).ToList());
	}


	[HttpPut("UpdateRecordValue")]
	public async Task<IActionResult> UpdateRecordValue([FromBody] UpdatePriceInformation information)
	{
		Result response = await _sender.Send(new UpdatePricingRecordCommand(information.Id, information.Price));

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response);
	}


	[HttpDelete("DeleteRecord")]
	public async Task<IActionResult> DeleteRecord([FromBody] long id)
	{
		Result response = await _sender.Send(new DeletePricingRecordCommand(id));

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response);
	}
}
