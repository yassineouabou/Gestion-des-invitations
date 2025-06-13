using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_Evenements_EvenementId",
                table: "Verifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_Visiteurs_VisiteurId",
                table: "Verifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_Evenements_EvenementId",
                table: "Verifications",
                column: "EvenementId",
                principalTable: "Evenements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_Visiteurs_VisiteurId",
                table: "Verifications",
                column: "VisiteurId",
                principalTable: "Visiteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_Evenements_EvenementId",
                table: "Verifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_Visiteurs_VisiteurId",
                table: "Verifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_Evenements_EvenementId",
                table: "Verifications",
                column: "EvenementId",
                principalTable: "Evenements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_Visiteurs_VisiteurId",
                table: "Verifications",
                column: "VisiteurId",
                principalTable: "Visiteurs",
                principalColumn: "Id");
        }
    }
}
