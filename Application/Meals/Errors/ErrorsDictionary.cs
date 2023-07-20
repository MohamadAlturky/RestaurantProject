//using SharedKernal.Utilities.Errors;

//namespace Application.Meals.Errors;
//public static class ErrorsDictionary
//{
//    public static class Meals
//    {
//        public static readonly Error NoMealAvailableNow =
//            new Error("No Meal Available", "Sorry We Didn't Cook Today");
//    }
//	public static class Balances
//	{
//		public static readonly Error NegativeBalanceDenied =
//			new Error("Negative Balance", "Sorry We Can't Create Or Update A Balance To Have A Negative Account");
//		public static readonly Error NegativeValueInput =
//			new Error("Negative Value Input", "Sorry We Can't Update The Balance With Negative Account");
//	}

//	public static class Customers
//	{
//		public static  Error DataBaseIgnoredToAddTheCustomer(string message) =>
//			new Error("DataBaseIgnoredToAddTheCustomer", message);

//		public static Error DataBaseIgnoredToIncreaseTheBalance(string message) =>
//			new Error("DataBaseIgnoredToAddTheCustomer", message);


//		public static  Error InvalidValueToAddToTheBalance() => 
//			new Error("InvalidValueToAddToTheBalance", "can't add zero or a negative value to the balance");
//	}
//}
