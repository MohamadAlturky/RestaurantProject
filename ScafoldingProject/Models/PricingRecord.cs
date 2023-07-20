namespace ScafoldingProject.Models;

public partial class PricingRecord
{
	public long Id { get; set; }
	public int Price { get; set; }
	public string CustomerTypeValue { get; set; } = null!;
	public string MealTypeValue { get; set; } = null!;
}
