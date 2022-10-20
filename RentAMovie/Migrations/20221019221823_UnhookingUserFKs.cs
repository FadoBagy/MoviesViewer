#nullable disable
namespace RentAMovie.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UnhookingUserFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_UserId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CreatorId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CreatorId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Movies_UserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Password = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatorId",
                table: "Reviews",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UserId",
                table: "Movies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_UserId",
                table: "Movies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CreatorId",
                table: "Reviews",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
