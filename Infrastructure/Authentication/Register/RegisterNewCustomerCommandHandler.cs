using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Infrastructure.Authentication.Register;
internal class RegisterNewCustomerCommandHandler : ICommandHandler<RegisterNewCustomerCommand>
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IUnitOfWork _unitOfWork;

	public RegisterNewCustomerCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
	{
		_userManager = userManager;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(RegisterNewCustomerCommand request, CancellationToken cancellationToken)
	{
		try
		{
			ApplicationUser user = new ApplicationUser()
			{
				Id = request.customer.SerialNumber.ToString(),
				CustomerId = request.customer.Id,
				UserName = Guid.NewGuid().ToString()
			};

			var result = await _userManager.CreateAsync(user, request.password);

			#warning needs refactor
			if (!result.Succeeded)
			{
				string res = "";
				foreach (var err in result.Errors)
				{
					res += err.Description;
				}
				throw new Exception(res);
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
