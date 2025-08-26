using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingWebApisWithAspNet.Migrations
{
    /// <inheritdoc />
    public partial class ChangePublisherToPublishers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGames_Publisher_PublisherID",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                schema: "BoardGame",
                table: "Publisher");

            migrationBuilder.RenameTable(
                name: "Publisher",
                schema: "BoardGame",
                newName: "Publishers",
                newSchema: "BoardGame");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                schema: "BoardGame",
                table: "Publishers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGames_Publishers_PublisherID",
                schema: "BoardGame",
                table: "BoardGames",
                column: "PublisherID",
                principalSchema: "BoardGame",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGames_Publishers_PublisherID",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                schema: "BoardGame",
                table: "Publishers");

            migrationBuilder.RenameTable(
                name: "Publishers",
                schema: "BoardGame",
                newName: "Publisher",
                newSchema: "BoardGame");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                schema: "BoardGame",
                table: "Publisher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGames_Publisher_PublisherID",
                schema: "BoardGame",
                table: "BoardGames",
                column: "PublisherID",
                principalSchema: "BoardGame",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
