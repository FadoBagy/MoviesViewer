#nullable disable
namespace RentAMovie.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UpdatingUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UserId",
                table: "Movies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_UserId",
                table: "Movies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_UserId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Movies_UserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "AspNetUsers");
        }
    }
}
