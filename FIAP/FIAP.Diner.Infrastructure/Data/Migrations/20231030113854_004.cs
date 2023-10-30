using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP.Diner.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class _004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE SEQUENCE sq_order_number START 1 MAXVALUE 10000000 INCREMENT 1;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP SEQUENCE sq_order_number;");
        }
    }
}
