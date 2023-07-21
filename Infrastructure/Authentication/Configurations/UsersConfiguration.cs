using Infrastructure.Authentication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Authentication.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("users");
		builder.HasKey(user => user.Id);
		builder.Property(user => user.SerialNumber); 
		builder.HasIndex(user => user.SerialNumber).IsUnique();
	}
}
