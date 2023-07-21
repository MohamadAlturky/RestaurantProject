using Domain.Customers.Aggregate;
using Infrastructure.Authentication.Models;
using Presentation.ApiModels.Register;

namespace Presentation.Factories;

public static class UserFactory
{
	public static User Create(RegistrationModel model)
	{
		Customer customer = new Customer()
		{
			Id = 0,
			SerialNumber = model.SerialNumber,
			BelongsToDepartment = model.BelongsToDepartment,
			Balance = model.Balance,
			Category = model.Category,
			Eligible = model.Eligible,
			FirstName = model.FirstName,
			LastName = model.LastName,
			IsActive = model.IsActive,
			IsRegular = model.IsRegular,
			Notes = model.Notes
		};

		List<Role> roles = new List<Role>();
		foreach (var id in model.Roles)
		{
			roles.Add(new Role()
			{
				Id = id
			});
		}

		return new User()
		{
			Id = 0,
			SerialNumber = customer.SerialNumber,
			Customer = customer,
			HiastMail = model.HiastMail,
			Roles = roles
		};
	}
}
