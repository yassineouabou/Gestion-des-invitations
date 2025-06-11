using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class secondeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Evenements");

            migrationBuilder.AddColumn<int>(
                name: "Participantes",
                table: "Evenements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participantes",
                table: "Evenements");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Evenements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
