﻿using Domain.Meals.Aggregate;
using Domain.Meals.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.UseCases.Meals.GetAll;
public class GetMealsQueryHandler : IQueryHandler<GetMealsQuery, List<Meal>>
{
	private readonly IUnitOfWork _unitOfWork;

	private readonly IMealRepository _mealRepository;

	public GetMealsQueryHandler(IUnitOfWork unitOfWork, IMealRepository mealRepository)
	{
		_unitOfWork = unitOfWork;
		_mealRepository = mealRepository;
	}

	public async Task<Result<List<Meal>>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
	{

		List<Meal> meals = _mealRepository.GetAll().ToList();


		if (meals == null || meals.Count() == 0)
		{
			return Result.Failure<List<Meal>>(new Error("",""));
		}

		await _unitOfWork.SaveChangesAsync();

		return Result.Success(meals);
	}

}

