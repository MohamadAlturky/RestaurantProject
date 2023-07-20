using Infrastructure.Authentication.JWTOptions;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication.JWTProvider;
public class JWTProvider : IJWTProvider
{
	private readonly JwtOptions _options;

	public JWTProvider(IOptions<JwtOptions> options)
	{
		_options = options.Value;
	}

	public string Generate(ApplicationUser user)
	{
		Claim[] Claims = new Claim[]
		{
			new Claim("id",user.Id)
		};

		SigningCredentials signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
			SecurityAlgorithms.HmacSha256);


		var token = new JwtSecurityToken(
			_options.Issuer,
			_options.Audience,
			Claims,
			null,
			DateTime.UtcNow.AddMinutes(30),
			signingCredentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
