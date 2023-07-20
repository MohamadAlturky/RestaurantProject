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
using Infrastructure.Models;
using Presentation.JWTOptionsSetup;
using Presentation.JWTBearerOptionsSetup;
using Infrastructure.Authentication.JWTProvider;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

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
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});
});




// JWT

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer();


builder.Services.AddScoped<IJWTProvider, JWTProvider>();



// identity with user manager

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
	options.SignIn.RequireConfirmedPhoneNumber = false;
	options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<RestaurantContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
	// Password settings.
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 0;
	options.Password.RequiredUniqueChars = 0;
	//// Lockout settings.
	//options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	//options.Lockout.MaxFailedAccessAttempts = 5;
	//options.Lockout.AllowedForNewUsers = true;

	// User settings.
	options.User.RequireUniqueEmail = false;
});


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
