namespace Domain.Shared.Utilities;
public static class Date
{
	private static DateOnly _toDay => new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
	public static DateTime ToDay { get => new DateTime(_toDay.Year, _toDay.Month, _toDay.Day); }
}
