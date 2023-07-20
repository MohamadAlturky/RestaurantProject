using Domain.Reservations.Aggregate;
using SharedKernal.Repositories;

namespace Domain.Reservations.Repositories;
public interface IReservationRepository : IRepository<Reservation>
{
	List<Reservation> GetByDate(DateOnly day);
	List<Reservation> GetBetweenTwoDate(DateOnly start, DateOnly end);
	bool CheckIfCustomerHasAMealReservation(long customerId, long mealEntryId);
	List<Reservation> GetByCustomer(long id);
}
