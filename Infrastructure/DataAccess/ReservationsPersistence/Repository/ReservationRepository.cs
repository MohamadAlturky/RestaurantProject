﻿using Domain.Reservations.Aggregate;
using Domain.Reservations.Repositories;
using Infrastructure.DataAccess.DBContext;

namespace Infrastructure.ReservationsPersistence.Repository;
public class ReservationRepository : IReservationRepository
{
	private readonly RestaurantContext _context;


	public ReservationRepository(RestaurantContext context)
	{
		_context = context;
	}



	public void Add(Reservation Entity)
	{
		_context.Set<Reservation>().Add(Entity);
	}

	public bool CheckIfCustomerHasAMealReservation(long customerId, long mealEntryId)
	{
		return _context.Set<Reservation>()
			   .Where(reservation => reservation.CustomerId == customerId)
			   .Where(reservation => reservation.MealEntryId == mealEntryId)
			   .Any();
	}

	public void Delete(Reservation Entity)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Reservation> GetAll()
	{
		throw new NotImplementedException();
	}

	public List<Reservation> GetBetweenTwoDate(DateOnly start, DateOnly end)
	{
		DateTime normalizedStart = new DateTime(start.Year, start.Month, start.Day);
		DateTime normalizedEnd = new DateTime(end.Year, end.Month, end.Day);

		return _context.Set<Reservation>()
			.Where(reservation => reservation.AtDay >= normalizedStart && reservation.AtDay <= normalizedEnd)
			.ToList();
	}

	public List<Reservation> GetByCustomer(long id)
	{
		return _context.Set<Reservation>()
			.Where(reservation => reservation.CustomerId == id)
			.ToList();
	}

	public List<Reservation> GetByDate(DateOnly day)
	{
		DateTime normalizedDay = new DateTime(day.Year, day.Month, day.Day);

		return _context.Set<Reservation>()
			.Where(reservation => reservation.AtDay == normalizedDay)
			.ToList();
	}

	public Reservation? GetById(long id)
	{
		return _context.Set<Reservation>().Find(id);
	}

	public IEnumerable<Reservation> GetPage(int pageSize, int pageNumber)
	{
		throw new NotImplementedException();
	}

	public void Update(Reservation Entity)
	{
		_context.Set<Reservation>().Update(Entity);
	}
}
