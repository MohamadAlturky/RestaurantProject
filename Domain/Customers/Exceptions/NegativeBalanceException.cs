namespace Domain.Customers.Exceptions;

public class NegativeBalanceException : Exception
{
	public NegativeBalanceException() 
		: base("the balance shouldn't be negative") { }
}