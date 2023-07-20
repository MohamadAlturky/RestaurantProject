using Infrastructure.Models;

namespace Infrastructure.Authentication.JWTProvider;
public interface IJWTProvider
{
	string Generate(ApplicationUser user);
}
