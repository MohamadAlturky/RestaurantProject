using Infrastructure.Authentication.Models;

namespace Infrastructure.Authentication.Permissions;
public static class PermissionsDictionary
{
	public static Permission ReadMeals = new()
	{
		Id = 1,
		Name = "ReadMeals"
	};

	public static Permission RegisterCustomer = new()
	{
		Id = 2,
		Name = "RegisterCustomer"
	};

	public static Permission CreateMeal = new()
	{
		Id = 3,
		Name = "CreateMeal"
	};
}
