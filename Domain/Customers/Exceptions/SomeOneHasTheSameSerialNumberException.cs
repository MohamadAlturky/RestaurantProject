namespace Domain.Customers.Exceptions;
public class SomeOneHasTheSameSerialNumberException : Exception
{
	public SomeOneHasTheSameSerialNumberException() : base()
	{

	}
	public SomeOneHasTheSameSerialNumberException(string message) : base(message)
	{

	}
}
