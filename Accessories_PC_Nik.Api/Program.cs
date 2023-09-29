using Accessories_PC_Nik.Context;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Implementations;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IClientsReadRepository, ClientsReadRepositories>();
builder.Services.AddSingleton<IAccessoriesContext, AccessoriesContext>();



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
