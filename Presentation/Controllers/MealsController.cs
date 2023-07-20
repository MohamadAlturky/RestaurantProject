using Application.Meals.UseCases.GetMealEntries;
using Application.Meals.UseCases.GetMealEntriesByDate;
using Application.Meals.UseCases.PrepareNewMeal;
using Application.UseCases.Meals.Create;
using Application.UseCases.Meals.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels.Meals;
using Presentation.Mappers;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MealsController : APIController
{
	private readonly ILogger<MealsController> _logger;

	public MealsController(ISender sender, IMapper mapper, ILogger<MealsController> logger) 
		: base(sender, mapper)
	{
		_logger = logger;
	}


	[HttpPost("Create")]
	public async Task<IActionResult> Create([FromBody] MealDTO meal)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
		}

		try
		{
			Result response = await _sender.Send(new CreateMealCommand(_mapper.Map(meal)));

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


	[HttpGet("GetAllMeals")]
	public async Task<IActionResult> GetAllMeals()
	{
		var response = await _sender.Send(new GetMealsQuery());

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response.Value.Select(meal => _mapper.Map(meal)).ToList());
	}



	[HttpGet("GetMealEntries")]
	public async Task<IActionResult> GetMealEntries(long Id)
	{
		var response = await _sender.Send(new GetMealEntriesQuery(Id));

		if (response.IsFailure)
		{
			return BadRequest(response.Error);
		}

		return Ok(response.Value);
	}

	[HttpGet("GetMealEntriesByDate")]
	public async Task<IActionResult> GetMealEntriesByDate(string date)
	{

		try
		{
			DateOnly dateFilter = DateOnly.Parse(date);

			var response = await _sender.Send(new GetMealEntriesByDateQuery(dateFilter));

			if (response.IsFailure)
			{
				return BadRequest(response.Error);
			}
			return Ok(response.Value.Select(entry => _mapper.Map(entry)));

		}
		catch(Exception exception)
		{
			return BadRequest(Result.Failure(new Error("", exception.Message)));
		}
	}


	[HttpPost("PrepareMeal")]
	public async Task<IActionResult> PrepareMeal(long mealId, string atDay, int numberOfUnits)
	{
		try
		{
			DateOnly atDayValue = DateOnly.Parse(atDay);
			Result response = await _sender.Send(new PrepareNewMealCommand(mealId, atDayValue, numberOfUnits));

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
}
