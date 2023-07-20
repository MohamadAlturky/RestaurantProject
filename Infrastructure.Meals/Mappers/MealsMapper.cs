using Domain.Meals.Aggregate;
using Domain.Meals.Entities;
using Domain.Meals.ValueObjects;
using Infrastructure.DataAccess.Models;

namespace Infrastructure.Meals.Mappers;
public class MealsMapper : IMealsMapper
{
	public Meal Map(DbMeal meal)
	{

		HashSet<PreparedMeal> preparedMeals = new HashSet<PreparedMeal>();

		foreach (DbMealEntry entry in meal.DbMealEntries)
		{
			preparedMeals.Add(this.Map(entry));
		}


		return new Meal(meal.Id,
			Enum.Parse<MealType>(meal.Type),
			meal.NumberOfCalories,
			meal.ImagePath,
			meal.Name,
			meal.Description,
			preparedMeals
			);
	}

	public DbMeal Map(Meal meal)
	{
		return new DbMeal()
		{
			Id = meal.Id,
			Name = meal.Name,
			Description = meal.Description,
			NumberOfCalories = meal.NumberOfCalories,
			ImagePath = meal.ImagePath,
			Type = meal.Type.ToString()
		};
	}

	public DbMealEntry Map(PreparedMeal meal)
	{
		throw new NotImplementedException();
	}

	public PreparedMeal Map(DbMealEntry meal)
	{
		return new PreparedMeal(meal.Id, 
			meal.MealId, 
			DateOnly.Parse(meal.AtDay.ToString()), 
			new NumberOfPreparedMeals(meal.NumberOfUnits),
			new NumberOfReservations(meal.NumberOfReservations), 
			meal.UserCanCancel);
	}
}
