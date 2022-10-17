using BussinessLogic.Logic;
using BussinessLogic.Persistence.Context;
using BussinessLogic.Persistence.Persistence;
using Core.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApi.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

// Scoped

builder.Services.AddScoped(typeof(IGenericPersistence<>), typeof(GenericPersistence<>));

builder.Services.AddScoped<IBillingService, BillingService>();

builder.Services.AddScoped<IProductService, ProductService>();

// Mapper

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Database Context
builder.Services.AddDbContext<StoreDbContext>(options =>
{
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    
    var context = services.GetRequiredService<StoreDbContext>();
    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();