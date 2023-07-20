using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.ApiModels.Meals;


public class MealDTO
{
	[AllowNull]
	public long Id { get; set; }


	[Required(ErrorMessage = "الرجاء إدخال نوع المنتج من فضلك")]
	public string Type { get; set; } = string.Empty;


	[Required(ErrorMessage = "الرجاء إدخال عدد السعرات الحرارية الخاصة بالمنتج من فضلك")]
	public int NumberOfCalories { get; set; }


	[AllowNull]
	public string ImagePath { get; set; } = string.Empty;



	[Required(ErrorMessage = "الرجاء إدخال إسم المنتج من فضلك")]
	public string Name { get; set; } = string.Empty;



	[Required(ErrorMessage = "الرجاء إدخال توصيف المنتج من فضلك")]
	public string Description { get; set; } = string.Empty;



	////[Required(ErrorMessage = "الرجاء إدخال صورة المنتج من فضلك")]
	//public IFormFile ImageFile { get; set; }
}

