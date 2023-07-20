using Infrastructure.Authentication.JWTProvider;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Infrastructure.Authentication.LogIn;
internal class LogInCommandHandler : ICommandHandler<LogInCommand, string>
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IJWTProvider _jWTProvider;

	public LogInCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IJWTProvider jWTProvider)
	{
		_userManager = userManager;
		_unitOfWork = unitOfWork;
		_jWTProvider = jWTProvider;
	}

	public async Task<Result<string>> Handle(LogInCommand request, CancellationToken cancellationToken)
	{
		try
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(request.model.SerialNumber.ToString());

			if (user is null)
			{
				throw new Exception("ApplicationUser? user = _userManager.FindByIdAsync(request.model.SerialNumber.ToString());");
			}

			bool checkPassword = await _userManager.CheckPasswordAsync(user, request.model.Password);

			if (!checkPassword)
			{
				throw new Exception("bool checkPassword = await _userManager.CheckPasswordAsync(user,request.model.Password);");
			}

			var token = _jWTProvider.Generate(user);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success(token);
		}
		catch (Exception exception)
		{
			return Result.Failure<string>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
