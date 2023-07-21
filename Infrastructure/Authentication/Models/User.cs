using Domain.Customers.Aggregate;

namespace Infrastructure.Authentication.Models;
public class User
{
	public long Id { get; set; }
	public long CustomerId { get; set; }
	public int SerialNumber { get; set; }
	public string HiastMail { get; set; } = string.Empty;
	public string HashedPassword { get; set; } = string.Empty;
	public string RefreshToken { get; set; } = string.Empty;
	public Customer Customer { get; set; } = new();

	public ICollection<Role> Roles = new List<Role>();
}
