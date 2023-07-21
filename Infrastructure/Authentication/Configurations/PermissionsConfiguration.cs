using Infrastructure.Authentication.Models;
using Infrastructure.Authentication.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Authentication.Configurations;
public class PermissionsConfiguration : IEntityTypeConfiguration<Permission>
{
	public void Configure(EntityTypeBuilder<Permission> builder)
	{
		builder.HasKey(permission=> permission.Id);

		IEnumerable<Permission> permissions = new List<Permission>()
		{
			PermissionsDictionary.CreateMeal,
			PermissionsDictionary.RegisterCustomer,
			PermissionsDictionary.ReadMeals
		};

		builder.HasData(permissions);
	}
}
