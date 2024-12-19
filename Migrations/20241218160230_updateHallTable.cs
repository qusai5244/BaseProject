using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseProject.Migrations
{
    /// <inheritdoc />
    public partial class updateHallTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Cinemas_CinemaId",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Halls_CinemaId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Halls");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_cinema_id",
                table: "Halls",
                column: "cinema_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Cinemas_cinema_id",
                table: "Halls",
                column: "cinema_id",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Cinemas_cinema_id",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Halls_cinema_id",
                table: "Halls");

            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "Halls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Halls_CinemaId",
                table: "Halls",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Cinemas_CinemaId",
                table: "Halls",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
