using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingWebApisWithAspNet.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesAndPublishersToBoardGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Flags",
                schema: "BoardGame",
                table: "Mechanics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "BoardGame",
                table: "Mechanics",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Flags",
                schema: "BoardGame",
                table: "Domains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "BoardGame",
                table: "Domains",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternateNames",
                schema: "BoardGame",
                table: "BoardGames",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desginer",
                schema: "BoardGame",
                table: "BoardGames",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Flags",
                schema: "BoardGame",
                table: "BoardGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PublisherID",
                schema: "BoardGame",
                table: "BoardGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "BoardGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                schema: "BoardGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardNameCategories",
                schema: "BoardGame",
                columns: table => new
                {
                    BoardGameId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardNameCategories", x => new { x.BoardGameId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BoardNameCategories_BoardGames_BoardGameId",
                        column: x => x.BoardGameId,
                        principalSchema: "BoardGame",
                        principalTable: "BoardGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardNameCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "BoardGame",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGames_PublisherID",
                schema: "BoardGame",
                table: "BoardGames",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_BoardNameCategories_CategoryId",
                schema: "BoardGame",
                table: "BoardNameCategories",
                column: "CategoryId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGames_Publisher_PublisherID",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.DropTable(
                name: "BoardNameCategories",
                schema: "BoardGame");

            migrationBuilder.DropTable(
                name: "Publisher",
                schema: "BoardGame");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "BoardGame");

            migrationBuilder.DropIndex(
                name: "IX_BoardGames_PublisherID",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.DropColumn(
                name: "Flags",
                schema: "BoardGame",
                table: "Mechanics");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "BoardGame",
                table: "Mechanics");

            migrationBuilder.DropColumn(
                name: "Flags",
                schema: "BoardGame",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "BoardGame",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "AlternateNames",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.DropColumn(
                name: "Desginer",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.DropColumn(
                name: "Flags",
                schema: "BoardGame",
                table: "BoardGames");

            migrationBuilder.DropColumn(
                name: "PublisherID",
                schema: "BoardGame",
                table: "BoardGames");
        }
    }
}
