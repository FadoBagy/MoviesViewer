namespace RentAMovie.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ReviewSpoiler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpoiler",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpoiler",
                table: "Reviews");
        }
    }
}
