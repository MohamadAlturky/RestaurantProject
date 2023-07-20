namespace Domain.Meals.Exceptions;

internal class NegativeNumberOfMealsException : Exception
{
	public NegativeNumberOfMealsException() 
		: base("the number of prepared meals value souldn't be negative")
	{
	}
}