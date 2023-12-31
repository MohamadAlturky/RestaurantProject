﻿using Domain.Meals.Aggregate;
using Domain.Meals.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.Meals.UseCases.PrepareNewMeal;
internal class PrepareNewMealCommandHandler : ICommandHandler<PrepareNewMealCommand>
{
	private IUnitOfWork _unitOfWork { get; set; }

	private IMealRepository _mealRepository { get; set; }

	public PrepareNewMealCommandHandler(IUnitOfWork unitOfWork, IMealRepository mealRepository)
	{
		_unitOfWork = unitOfWork;
		_mealRepository = mealRepository;
	}

	public async Task<Result> Handle(PrepareNewMealCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Meal? meal = _mealRepository.GetById(request.mealId);
			if(meal is null)
			{
				#warning needs refactor
				throw new Exception("no meal found");
			}

			#warning needs to be filtered
			
			DateTime date = new DateTime(request.atDay.Year, 
				request.atDay.Month,
				request.atDay.Day);

			meal.PrepareNewEntry(date, request.numberOfUnits);

			_mealRepository.Update(meal);

			await _unitOfWork.SaveChangesAsync();
		}
		catch (Exception exception)
		{
			return Result.Failure(new Error("", exception.Message));
		}

		return Result.Success();
	}
}