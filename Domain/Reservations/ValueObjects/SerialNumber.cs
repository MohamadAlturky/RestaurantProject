using SharedKernal.ValueObjects;

namespace Domain.Reservations.ValueObjects;
public class SerialNumber : ValueObject<string>
{
	public SerialNumber(string value) : base(value) { }

	protected override void Validate(string value)
	{
		// no validation needed
	}
}
