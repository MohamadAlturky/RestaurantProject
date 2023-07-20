using SharedKernal.CQRS.Commands;

namespace Application.Reservations.UseCases.Create;
public sealed record CreateReservationCommand(long customerId, long orderedMealId) : ICommand;
 