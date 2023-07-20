namespace SharedKernal.DomainEvents;

public interface IDomainEventDispatcher
{
	Task Dispatch(IDomainEvent domainEvent);
}
