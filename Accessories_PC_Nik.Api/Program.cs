using Accessories_PC_Nik.Api.Infrastructures;
using Accessories_PC_Nik.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<AccessoriesExceptionFilter>();
}).AddControllersAsServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.GetSwaggerDocument();

builder.Services.AddDependences();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<AccessoriesContext>(options => options.UseSqlServer(conString),
    ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.GetSwaggerDocumentUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }