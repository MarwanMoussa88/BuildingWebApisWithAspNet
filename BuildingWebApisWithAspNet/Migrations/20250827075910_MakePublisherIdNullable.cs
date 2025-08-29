using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingWebApisWithAspNet.Migrations
{
    /// <inheritdoc />
    public partial class MakePublisherIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGames_Publishers_PublisherId",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                schema: "BoardGame",
                table: "BoardGames",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGames_Publishers_PublisherId",
                schema: "BoardGame",
                table: "BoardGames",
                column: "PublisherId",
                principalSchema: "BoardGame",
                principalTable: "Publishers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGames_Publishers_PublisherId",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                schema: "BoardGame",
                table: "BoardGames",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGames_Publishers_PublisherId",
                schema: "BoardGame",
                table: "BoardGames",
                column: "PublisherId",
                principalSchema: "BoardGame",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
