using Domain.Meals.Aggregate;
using Domain.Meals.Repositories;
using Domain.Shared.Entities;
using Infrastructure.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using SharedKernal.Entities;
using System;

namespace Infrastructure.MealsPersistence.Repository;

public class MealRepository : IMealRepository
{
	private readonly RestaurantContext _context;

	public MealRepository(RestaurantContext context)
	{
		_context = context;
	}


	public void Add(Meal Entity)
	{
		// we will make the id = 0 to let EF Core set the primary key with auto increament value
		Entity.Id = 0;

		_context.Set<Meal>().Add(Entity);
	}

	public void Delete(Meal Entity)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Meal> GetAll()
	{
		return _context.Set<Meal>().ToList();
	}

	public List<MealEntry> GetEntriesByDate(DateOnly date)
	{
		DateTime dateTime = new DateTime(date.Year, date.Month, date.Day);
		return _context.Set<MealEntry>()
			.Where(entry => entry.AtDay == dateTime)
			.Include(entry => entry.Meal)
			.AsNoTracking()
			.ToList();
	}

	public Meal? GetById(long id)
	{
		return _context.Set<Meal>().Find(id);
	}

	public IEnumerable<MealEntry> GetMealEntries(long mealId)
	{
		return _context.Set<MealEntry>()
			.Where(mealEntry => mealEntry.MealId == mealId)
			.AsNoTracking()
			.ToList();
	}

	public IEnumerable<Meal> GetPage(int pageSize, int pageNumber)
	{
		throw new NotImplementedException();
	}


	public void Update(Meal Entity)
	{
		_context.Set<Meal>().Update(Entity);
	}

	public Meal? GetMealWithEntry(long id, Func<MealEntry, bool> entrySelector)
	{
		return _context.Set<Meal>()
			   .Where(meal => meal.Id == id)
			   .Include(meal => meal.MealEntries
				   .Where(entry => entrySelector(entry)))
			   .FirstOrDefault();
	}

	public MealEntry? GetMealEntry(long entryId)
	{
		return _context.Set<MealEntry>()
			.Include(entry=> entry.Meal)
			.Where(entry=> entry.Id==entryId).FirstOrDefault();
	}
}
