using Infrastructure.Authentication.JWTOptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Presentation.JWTBearerOptionsSetup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
	private readonly JwtOptions _jwtOptions;

	public JwtBearerOptionsSetup(IOptions<JwtOptions> options)
	{
		_jwtOptions = options.Value;
	}

	public void Configure(JwtBearerOptions options)
	{
		options.TokenValidationParameters = new()
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = _jwtOptions.Issuer,
			ValidAudience = _jwtOptions.Audience,
			IssuerSigningKey = 
			new SymmetricSecurityKey(Encoding.UTF8.
			GetBytes(_jwtOptions.SecretKey)),
		};
	}
}
