using Domain.Reservations.Exceptions;
using SharedKernal.ValueObjects;

namespace Domain.Reservations.ValueObjects;


public class Balance : ValueObject<int>
{
	public Balance(int value) : base(value) { }

	protected override void Validate(int value)
	{
		if(Value < 0)
		{
			throw new NegativeBalanceException();
		}
	}
}
