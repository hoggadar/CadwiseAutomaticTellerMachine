using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadwiseAutomaticTellerMachine.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCardModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_AuthorId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_AuthorId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Cards");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_UserId",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Cards",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AuthorId",
                table: "Cards",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_AuthorId",
                table: "Cards",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
