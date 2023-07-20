using SharedKernal.ValueObjects;

namespace Domain.Shared.ValueObjects;
public class NumberOfReservations : ValueObject<int>
{
	public NumberOfReservations(int value) : base(value) { }

	protected override void Validate(int value)
	{
		if (value < 0)
		{
			throw new ArgumentOutOfRangeException("NumberOfReservations have negative value");
		}
	}
}
