using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Pagination;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositoryPagination<Breed>, Repository<Breed>>();
builder.Services.AddScoped<BreedsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
