using Serilog;
using Microsoft.EntityFrameworkCore;
using FIAP.Diner.Infrastructure.Configuration;
using FIAP.Diner.Infrastructure.Data;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddLogger();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<Context>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    Log.Information("Application ready to start");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

return 0;