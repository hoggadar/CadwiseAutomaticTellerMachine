using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadwiseAutomaticTellerMachine.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCashType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Cash",
                table: "Cards",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cash",
                table: "Cards",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
