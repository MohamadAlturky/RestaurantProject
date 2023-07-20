using Domain.Meals.Repositories;
//using Infrastructure.DataAccess.UnitOfWork;
using MediatR;
//using Presentation.Mappers;
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
	typeof(ApplicationAssemblyReference).Assembly};

builder.Services.AddMediatR(assembliesForConfigureMediatR);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
