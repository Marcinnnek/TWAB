using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TWAB.Application.Common.Persistence.Base;
using TWAB.Infrastructure.Persistence.Persistence;
using TWAB.Infrastructure.Persistence.Persistence.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

builder.Services.AddDbContext<DataContext>(options => 
{
    options.UseSqlite(config.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("TWAB.Api"));
});

builder.Services.AddScoped<IRepository<User>, Repository<User,DataContext>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
