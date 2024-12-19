using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseProject.Migrations
{
    /// <inheritdoc />
    public partial class upMovieTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Halls_hid",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_hid",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "hid",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Movies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "hid",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "Movies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_hid",
                table: "Movies",
                column: "hid");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Halls_hid",
                table: "Movies",
                column: "hid",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
