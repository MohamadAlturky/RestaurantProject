using Domain.Customers.Aggregate;
using Microsoft.AspNetCore.Identity;
namespace Infrastructure.Models;


public class ApplicationUser : IdentityUser<string>
{
	public long CustomerId { get; set; }
	public virtual Customer Customer { get; set; }
}
