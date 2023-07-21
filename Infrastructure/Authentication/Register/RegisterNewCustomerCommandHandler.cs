using Domain.Customers.Aggregate;
using Infrastructure.Authentication.Models;
using Infrastructure.DataAccess.DBContext;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Infrastructure.Authentication.Register;
internal class RegisterNewCustomerCommandHandler : ICommandHandler<RegisterNewCustomerCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly RestaurantContext _context;

	public RegisterNewCustomerCommandHandler(IUnitOfWork unitOfWork, RestaurantContext context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
	}

	public async Task<Result> Handle(RegisterNewCustomerCommand request, 
		CancellationToken cancellationToken)
	{
		try
		{
			User user = request.user;
			Customer customer = request.user.Customer;
			string password = request.password;

			_context.Set<Customer>().Add(customer);
			await _unitOfWork.SaveChangesAsync();

			user.Customer = null;
			user.CustomerId = customer.Id;

			#warning you should add hashed stuff

			List<Role> roles = user.Roles.ToList();
			user.Roles = null;
			
			_context.Set<User>().Add(user);
			await _unitOfWork.SaveChangesAsync();


			foreach (var role in roles)
			{
				_context.Set<UserRole>().Add(new UserRole()
				{
					UserId = user.Id,
					RoleId = role.Id
				});
			}
			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
