using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;
using Domain.Meals.Repositories;
using Domain.Reservations.Aggregate;
using Domain.Reservations.Repositories;
using Domain.Shared.Entities;
using Domain.Shared.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Reservations.UseCases.Create;
internal class CreateReservationCommandHandler : ICommandHandler<CreateReservationCommand>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;
	private readonly IMealRepository _mealRepository;
	private readonly ICustomerRepository _customerRepository;
	private readonly IPricingRepository _pricingRepository;

	public CreateReservationCommandHandler(IUnitOfWork unitOfWork,
		IReservationRepository reservationRepository,
		IMealRepository mealRepository,
		ICustomerRepository customerRepository,
		IPricingRepository pricingRepository)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
		_mealRepository = mealRepository;
		_customerRepository = customerRepository;
		_pricingRepository = pricingRepository;
	}

	public async Task<Result> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			MealEntry? entry = _mealRepository.GetMealEntry(request.orderedMealId);

			if (entry is null || entry.Meal is null)
			{
				throw new Exception("MealEntry? entry = _mealRepository.GetMealEntry(request.orderedMealId);");
			}

			Customer? customer = _customerRepository.GetById(request.customerId);


			if (customer is null)
			{
				throw new Exception("Customer? customer = _customerRepository.GetById(request.customerId);");
			}

			PricingRecord? pricingRecord = _pricingRepository.GetByFilter(customer.Category, entry.Meal.Type);

			if (pricingRecord is null)
			{
				throw new Exception("PricingRecord? record = _pricingRepository.GetByFilter(record => record.CustomerTypeValue == customer.Category && record.MealTypeValue == entry.Meal.Type);");
			}

			if (_reservationRepository.CheckIfCustomerHasAMealReservation(request.customerId, request.orderedMealId))
			{
				throw new Exception("CheckIfCustomerHasAMealReservation is true");
			}

			_reservationRepository.Add(
					Reservation.Create(
						entry,
						customer,
						pricingRecord,
						request.customerId,
						request.orderedMealId));

			await _unitOfWork.SaveChangesAsync();
		}

		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
		return Result.Success();
	}
}
