using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using FIAP.Diner.API.Configuration;
using FIAP.Diner.Domain.Abstractions;
using FIAP.Diner.Infrastructure.Configuration;
using FIAP.Diner.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Host.AddLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Byte Burguer",
        Version = "1.0.0",
        Description = "Sistema de gerenciamento para a lanchonete Byte Burguer"
    });
});

builder.Services.AddData(builder.Configuration);

builder.Services.AddDependencyInjection();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        var exceptionHandlerFeatures = context.Features.Get<IExceptionHandlerFeature>();

        if (exceptionHandlerFeatures != null)
        {
            var exception = exceptionHandlerFeatures.Error;
            var message = exception.Message;

            if (exception is DomainException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.Response.ContentType = "application/json";

            var json = new
            {
                statusCode = context.Response.StatusCode,
                message = message,
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(json));
        }
    });
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();