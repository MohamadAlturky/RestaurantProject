namespace Domain.Shared.Exceptions;

public class NegativePriceException : Exception
{
	public NegativePriceException()
		: base("Sorry the price can't be negative.") { }
}
