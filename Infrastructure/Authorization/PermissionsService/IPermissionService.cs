namespace Infrastructure.Authorization.PermissionsService;
public interface IPermissionService
{
	Task<HashSet<string>> GetPermissions(long userId);
}
