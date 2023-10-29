namespace FIAP.Diner.Tests.Infrastructure.Data;

public class ContextTest
{
    [Fact]
    public void OnConfiguring_WithValidConnectionString_SetsConnectionString()
    {
        // Arrange
        var configuration = Substitute.For<DbContextOptions<Context>>();
        const string connectionString =
            "Host=localhost; Database=fiap-diner; Username=postgres; Password=admin";


        var context = new TestContext(configuration);
        var optionsBuilder = new DbContextOptionsBuilder<Context>();

        // Act
        context.OnConfiguring(optionsBuilder);

        // Assert
        optionsBuilder.Options.Should().BeOfType<DbContextOptions<Context>>();
        var contextOptions = optionsBuilder.Options;
        contextOptions.Extensions.Should().ContainSingle(e =>
            e is NpgsqlOptionsExtension &&
            ((NpgsqlOptionsExtension)e).ConnectionString == connectionString);
    }

    [Fact]
    public void OnConfiguring_WithNullConnectionString_ThrowsArgumentNullException()
    {
        // Arrange
        var configuration = Substitute.For<DbContextOptions<Context>>();

        var context = new TestContext(configuration);

        // Act
        var act = () => context.Database.OpenConnection();

        // Assert
        act.Should().Throw<Exception>();
    }

    private class TestContext : Context
    {
        public TestContext(DbContextOptions<Context> options) : base(options) { }

        public new void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            base.OnConfiguring(optionsBuilder);
    }
}