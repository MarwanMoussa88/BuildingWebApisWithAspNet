using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingWebApisWithAspNet.Migrations
{
    /// <inheritdoc />
    public partial class changeBoardGameMechanicstablenameandschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BoardGameMechanics",
                newName: "BoardGameMechanics",
                newSchema: "BoardGame");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BoardGameMechanics",
                schema: "BoardGame",
                newName: "BoardGameMechanics");
        }
    }
}
