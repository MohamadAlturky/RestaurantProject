using SharedKernal.Repositories;
using Domain.Meals.Aggregate;
using Domain.Shared.Entities;

namespace Domain.Meals.Repositories;

public interface IMealRepository : IRepository<Meal>
{
	IEnumerable<MealEntry> GetMealEntries(long mealId);
	public MealEntry? GetMealEntry(long entryId);
	List<MealEntry> GetEntriesByDate(DateOnly date);
	Meal? GetMealWithEntry(long id,Func<MealEntry,bool> entrySelector);
}
