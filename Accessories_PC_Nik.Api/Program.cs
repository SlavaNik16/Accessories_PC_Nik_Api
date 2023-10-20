using Accessories_PC_Nik.Api.Infrastructures;
using Accessories_PC_Nik.Context;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Implementations;
using Accessories_PC_Nik.Services.Automappers;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.GetSwaggerDocument();


builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IClientsReadRepository, ClientsReadRepositories>();

builder.Services.AddScoped<IWorkersService, WorkersService>();
builder.Services.AddScoped<IWorkersReadRepository, WorkersReadRepository>();

builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IServicesReadRepository, ServicesReadRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();

builder.Services.AddScoped<IComponentsService, ComponentsService>();
builder.Services.AddScoped<IComponentsReadRepository, ComponentsReadRepository>();

builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IDeliveryReadRepository, DeliveryReadRepository>();

builder.Services.AddSingleton<IAccessoriesContext, AccessoriesContext>();

builder.Services.AddAutoMapper(typeof(ServiceProfile));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.GetSwaggerDocumentUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
