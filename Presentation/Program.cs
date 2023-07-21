using Domain.Meals.Repositories;
using MediatR;
using SharedKernal.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Application;
using Infrastructure.MealsPersistence.Repository;
using Infrastructure.ReservationsPersistence.Repository;
using Domain.Reservations.Repositories;
using Domain.Customers.Repositories;
using Infrastructure.CustomersPersistance.Repository;
using Infrastructure.DataAccess.DBContext;
using Infrastructure.DataAccess.UnitOfWork;
using Presentation.Mappers;
using Domain.Shared.Repositories;
using Infrastructure.PricingRecordsPersistance.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation.JWTOptionsSetup;
using Infrastructure.Authentication.JWTProvider;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Authorization.AuthorizationPolicyProvider;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Authorization.AuthorizationHandlers;
using Infrastructure.Authorization.PermissionsService;

var builder = WebApplication.CreateBuilder(args);

// Persistance level
builder.Services.AddDbContext<RestaurantContext>(option =>
			option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPricingRepository, PricingRepository>();



//// Presentation level
builder.Services.AddScoped<IMapper, Mapper>();

Assembly[] assembliesForConfigureMediatR = new Assembly[]
{
	typeof(ApplicationAssemblyReference).Assembly,
	typeof(InfrastructureAssemblyReference).Assembly
};

builder.Services.AddMediatR(assembliesForConfigureMediatR);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Auth",
		Version = "v1"
	});
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "please",
		Name = "auth",
		Type = SecuritySchemeType.ApiKey,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme()
			{
				Reference = new OpenApiReference()
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{ }
		}
	});
});




// JWT

builder.Services.ConfigureOptions<JwtOptionsSetup>();
//builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//	.AddJwtBearer(options=>
//				  options.TokenValidationParameters = new()
//				  {
//				  	ValidateIssuer = false,
//				  	//ValidateAudience = true,
//				  	ValidateLifetime = true,
//				  	ValidateIssuerSigningKey = true,
//				  	ValidIssuer = "Alkhall",
//				  	ValidAudience = "Jaffar",
//				  	IssuerSigningKey =
//				  		new SymmetricSecurityKey(Encoding.UTF8.
//				  		GetBytes("_AboRamezSecretKey12345"))
//				  }
//);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

			   .AddJwtBearer(options =>
			   {
				   options.TokenValidationParameters = new TokenValidationParameters
				   {
					   ValidateIssuerSigningKey = true,
					   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("AboRamezSecretKey12345")),
					   ValidateIssuer = false,
					   ValidateAudience = false,
					   RequireExpirationTime = false,
					   ValidateLifetime = true
				   };
			   });


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//	.AddJwtBearer();
builder.Services.AddScoped<IJWTProvider, JWTProvider>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
