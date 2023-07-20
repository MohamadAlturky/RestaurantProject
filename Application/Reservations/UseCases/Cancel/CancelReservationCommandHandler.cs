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

namespace Application.Reservations.UseCases.Cancel;
internal class CancelReservationCommandHandler : ICommandHandler<CancelReservationCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;
	private readonly IMealRepository _mealRepository;
	private readonly ICustomerRepository _customerRepository;
	private readonly IPricingRepository _pricingRepository;

	public CancelReservationCommandHandler(IUnitOfWork unitOfWork, IReservationRepository reservationRepository, IMealRepository mealRepository, ICustomerRepository customerRepository, IPricingRepository pricingRepository)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
		_mealRepository = mealRepository;
		_customerRepository = customerRepository;
		_pricingRepository = pricingRepository;
	}

	public async Task<Result> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Reservation? reservation = _reservationRepository.GetById(request.id);

			if (reservation is null)
			{
				throw new ArgumentException("Reservation? reservation = _reservationRepository.GetById(request.id);");
			}

			MealEntry? entry = _mealRepository.GetMealEntry(reservation.MealEntryId);

			if (entry is null)
			{
				throw new ArgumentException("MealEntry? entry = _mealRepository.GetMealEntry(reservation.MealEntryId);");
			}

			Customer? customer = _customerRepository.GetById(reservation.CustomerId);

			if (customer is null)
			{
				throw new ArgumentException("Customer? customer = _customerRepository.GetById(reservation.CustomerId);");
			}

			reservation.Cancel(entry, customer);

			_reservationRepository.Update(reservation);

			await _unitOfWork.SaveChangesAsync();
		}

		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
		return Result.Success();
	}
}
