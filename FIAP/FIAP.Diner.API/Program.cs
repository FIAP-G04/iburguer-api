using FIAP.Diner.API.Configuration;
using FIAP.Diner.Infrastructure.Configuration;
using FIAP.Diner.Infrastructure.Data.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddData(builder.Configuration);

builder.Services.AddDependencyInjection();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();