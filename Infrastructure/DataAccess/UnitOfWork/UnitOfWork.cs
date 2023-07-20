using Infrastructure.DataAccess.DBContext;
using SharedKernal.Repositories;

namespace Infrastructure.DataAccess.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
	private readonly RestaurantContext _context;

	public UnitOfWork(RestaurantContext context)
	{
		_context = context;
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public Task SaveChangesAsync()
	{
		return _context.SaveChangesAsync();
	}
}
