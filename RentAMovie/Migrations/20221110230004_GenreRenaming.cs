#nullable disable
namespace RentAMovie.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class GenreRenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genres_TmdbGenresId",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie");

            migrationBuilder.DropIndex(
                name: "IX_GenreMovie_TmdbGenresId",
                table: "GenreMovie");

            migrationBuilder.RenameColumn(
                name: "TmdbGenresId",
                table: "GenreMovie",
                newName: "GenresCollectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie",
                columns: new[] { "GenresCollectionId", "MoviesId" });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "GenreMovie",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genres_GenresCollectionId",
                table: "GenreMovie",
                column: "GenresCollectionId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genres_GenresCollectionId",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie");

            migrationBuilder.DropIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "GenreMovie");

            migrationBuilder.RenameColumn(
                name: "GenresCollectionId",
                table: "GenreMovie",
                newName: "TmdbGenresId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie",
                columns: new[] { "MoviesId", "TmdbGenresId" });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_TmdbGenresId",
                table: "GenreMovie",
                column: "TmdbGenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genres_TmdbGenresId",
                table: "GenreMovie",
                column: "TmdbGenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
