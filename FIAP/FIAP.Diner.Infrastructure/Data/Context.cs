using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FIAP.Diner.Infrastructure.Data;

public class Context : DbContext
{
    private readonly IConfiguration configuration;

    public Context(IConfiguration configuration) => this.configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
}