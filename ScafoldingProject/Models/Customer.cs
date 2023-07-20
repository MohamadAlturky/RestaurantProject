namespace ScafoldingProject.Models;

public partial class Customer
{
	public Customer()
	{
		Reservations = new HashSet<Reservation>();
	}

	public long Id { get; set; }
	public int SerialNumber { get; set; }
	public int Balance { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Category { get; set; } = null!;
	public string BelongsToDepartment { get; set; } = null!;
	public string Notes { get; set; } = null!;
	public bool IsRegular { get; set; }
	public bool Eligible { get; set; }
	public bool IsActive { get; set; }

	public virtual ICollection<Reservation> Reservations { get; set; }
}
