using Infrastructure.Authentication.Models;

namespace Infrastructure.Authentication.JWTProvider;
public interface IJWTProvider
{
	string Generate(User user);
}
