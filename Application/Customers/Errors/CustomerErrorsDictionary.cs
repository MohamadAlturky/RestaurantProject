using SharedKernal.Utilities.Errors;

namespace Application.Customers.Errors;
public static class CustomerErrorsDictionary
{
	public static Error DataBaseIgnoredToAddTheCustomer(string message) =>
		new Error("خطأ في قاعدة معطيات" + "," +
			".حصل خطأ عند محاولة حفظ الزبون تأكد من القيم المعطاة وحاول مرة أخرى", message);

	internal static Error SomeOneHasTheSameSerialNumber(string message) =>
		new Error("هناك شخص موجود بلفعل يحمل نفس الرقم الذاتي حاول مرة أخرى",message);

	public static Error DataBaseIgnoredToIncreaseTheBalance(string message) =>
		new Error("DataBaseIgnoredToAddTheCustomer", message);

	public static Error InvalidValueToAddToTheBalance() =>
		new Error("InvalidValueToAddToTheBalance", "لا يمكن إضافة قيمة سالبة لحساب شخص");
}

