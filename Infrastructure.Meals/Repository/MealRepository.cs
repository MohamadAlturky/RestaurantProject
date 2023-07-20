using Domain.Meals.Aggregate;
using Domain.Meals.Repositories;
using Infrastructure.DataAccess.Models;
using Infrastructure.Meals.Mappers;

namespace Infrastructure.Meals.Repository;

public class MealRepository : IMealRepository
{
	private readonly RestaurantContext _context;

	private readonly IMealsMapper _mealsMapper;


	public MealRepository(RestaurantContext context, IMealsMapper mealsMapper)
	{
		_context = context;
		_mealsMapper = mealsMapper;
	}


	public void Add(Meal Entity)
	{
		DbMeal dbMeal = _mealsMapper.Map(Entity);

		// we will make the id = 0 to let EF Core set the primary key with auto increament value

		dbMeal.Id = 0;

		_context.Set<DbMeal>().Add(dbMeal);
	}

	public void SavePreparedMeals(Meal meal)
	{
		throw new NotImplementedException();
	}

	public void Delete(Meal Entity)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Meal> GetAll()
	{
		throw new NotImplementedException();
	}

	public Meal? GetById(int id)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Meal> GetPage()
	{
		throw new NotImplementedException();
	}

	public void Update(Meal Entity)
	{
		throw new NotImplementedException();
	}

	public void ModifyCancellationPermissionForThePreparedMeals(Meal meal)
	{
		throw new NotImplementedException();
	}
}
