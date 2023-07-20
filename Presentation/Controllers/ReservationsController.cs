using Application.Reservations.UseCases.Cancel;
using Application.Reservations.UseCases.Create;
using Application.Reservations.UseCases.GetBetweenTwoDates;
using Application.Reservations.UseCases.GetByCustomerSerialNumber;
using Application.Reservations.UseCases.GetByDate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels.Reservations;
using Presentation.Mappers;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReservationsController : APIController
{
	private readonly ILogger<ReservationsController> _logger;

	public ReservationsController(ILogger<ReservationsController> logger, ISender sender, IMapper mapper)
		: base(sender, mapper)
	{
		_logger = logger;
	}


	[HttpPost("Create")]
	public async Task<IActionResult> Create([FromBody] ReservationInformation information)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
		}

		try
		{
			Result response = await _sender.Send(new CreateReservationCommand(information.customerId, information.orderedMealId));

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

	[HttpPost("Cancel")]
	public async Task<IActionResult> Cancel([FromBody] long reservationId)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
		}

		try
		{
			Result response = await _sender.Send(new CancelReservationCommand(reservationId));

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

	[HttpGet("GetReservationsByDate")]
	public async Task<IActionResult> GetReservationsByDate(string date)
	{
		try
		{
			DateOnly dateFilter = DateOnly.Parse(date);

			var response = await _sender.Send(new GetReservationsByDateQuery(dateFilter));

			if (response.IsFailure)
			{
				return BadRequest(response.Error);
			}
			return Ok(response.Value);

		}
		catch (Exception exception)
		{
			return BadRequest(Result.Failure(new Error("", exception.Message)));
		}
	}

	[HttpGet("GetReservationsBetweenTwoDates")]
	public async Task<IActionResult> GetReservationsBetweenTwoDates(string startDate, string endDate)
	{
		try
		{
			DateOnly start = DateOnly.Parse(startDate);
			DateOnly end = DateOnly.Parse(endDate);

			if(start > end)
			{
				throw new Exception("start > end");
			}

			var response = await _sender.Send(new GetReservationsBetweenTwoDatesQuery(start,end));

			if (response.IsFailure)
			{
				return BadRequest(response.Error);
			}
			return Ok(response.Value);

		}
		catch (Exception exception)
		{
			return BadRequest(Result.Failure(new Error("", exception.Message)));
		}
	}




	[HttpGet("GetReservationsByCustomerSerialNumber")]
	public async Task<IActionResult> GetReservationsByCustomerSerialNumber(int serialNumber)
	{
		try
		{
			var response = await _sender.Send(new GetReservationsByCustomerSerialNumberQuery(serialNumber));

			if (response.IsFailure)
			{
				return BadRequest(response.Error);
			}
			return Ok(response.Value);

		}
		catch (Exception exception)
		{
			return BadRequest(Result.Failure(new Error("", exception.Message)));
		}
	}
}
