using Infrastructure.Authentication.Claims;
using Infrastructure.Authorization.PermissionsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authorization.AuthorizationHandlers;

// this service will be singelton
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
	{
		string? userId = context.User.Claims
			.FirstOrDefault(claim => claim.Type == CustomClaims.SerialNumber)?.Value;

		if(!long.TryParse(userId, out long parsedId))
		{
			return;
		}
		
		using IServiceScope scope = _serviceScopeFactory.CreateScope();
		
		IPermissionService permissionService = scope.ServiceProvider
			.GetRequiredService<IPermissionService>();

		HashSet<string> permissions = await permissionService.GetPermissions(parsedId);

		if (permissions.Contains(requirement.Permission))
		{
			context.Succeed(requirement);
		}
	}
}
