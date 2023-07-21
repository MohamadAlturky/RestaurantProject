using Infrastructure.Authentication.Models;
using Infrastructure.Authentication.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Authentication.Configurations;
public class RolesPermissionsConfiguration : IEntityTypeConfiguration<RolePermission>
{
	public void Configure(EntityTypeBuilder<RolePermission> builder)
	{
		builder.HasKey(entity => new { entity.RoleId, entity.PermissionId });

		IEnumerable<RolePermission> rolePermissions = new List<RolePermission>()
		{
			Create(RolesDictionary.Manager,PermissionsDictionary.ReadMeals),
			Create(RolesDictionary.Manager,PermissionsDictionary.RegisterCustomer),
			Create(RolesDictionary.Manager,PermissionsDictionary.CreateMeal),
			Create(RolesDictionary.User,PermissionsDictionary.ReadMeals)
		};

		builder.HasData(rolePermissions);
	}
	private static RolePermission Create(Role role, Permission permission)
	{
		return new RolePermission()
		{
			PermissionId = permission.Id,
			RoleId = role.Id
		};
	}
}
