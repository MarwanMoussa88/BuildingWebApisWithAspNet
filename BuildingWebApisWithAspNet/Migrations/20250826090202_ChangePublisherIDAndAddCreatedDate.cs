using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingWebApisWithAspNet.Migrations
{
    /// <inheritdoc />
    public partial class ChangePublisherIDAndAddCreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGames_Publishers_PublisherID",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.RenameColumn(
                name: "PublisherID",
                schema: "BoardGame",
                table: "BoardGames",
                newName: "PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_BoardGames_PublisherID",
                schema: "BoardGame",
                table: "BoardGames",
                newName: "IX_BoardGames_PublisherId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "BoardGame",
                table: "BoardNameCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGames_Publishers_PublisherId",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "BoardGame",
                table: "BoardNameCategories");

            migrationBuilder.RenameColumn(
                name: "PublisherId",
                schema: "BoardGame",
                table: "BoardGames",
                newName: "PublisherID");

            migrationBuilder.RenameIndex(
                name: "IX_BoardGames_PublisherId",
                schema: "BoardGame",
                table: "BoardGames",
                newName: "IX_BoardGames_PublisherID");

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
    }
}
