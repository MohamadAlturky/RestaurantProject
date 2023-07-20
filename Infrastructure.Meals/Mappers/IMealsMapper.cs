using Domain.Meals.Aggregate;
using Domain.Meals.Entities;
using Infrastructure.DataAccess.Models;

namespace Infrastructure.Meals.Mappers;
public interface IMealsMapper
{
	Meal Map(DbMeal meal);
	DbMeal Map(Meal meal);

	DbMealEntry Map(PreparedMeal meal);
	PreparedMeal Map(DbMealEntry meal);

}
