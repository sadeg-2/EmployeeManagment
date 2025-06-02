using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Library.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatesanction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanactionTypeId",
                table: "Sanctions");

            migrationBuilder.DropIndex(
                name: "IX_Sanctions_SanactionTypeId",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "SanactionTypeId",
                table: "Sanctions");

            migrationBuilder.AddColumn<int>(
                name: "SanctionTypeId",
                table: "Sanctions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sanctions_SanctionTypeId",
                table: "Sanctions",
                column: "SanctionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions",
                column: "SanctionTypeId",
                principalTable: "SanctionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions");

            migrationBuilder.DropIndex(
                name: "IX_Sanctions_SanctionTypeId",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "SanctionTypeId",
                table: "Sanctions");

            migrationBuilder.AddColumn<int>(
                name: "SanactionTypeId",
                table: "Sanctions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sanctions_SanactionTypeId",
                table: "Sanctions",
                column: "SanactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanactionTypeId",
                table: "Sanctions",
                column: "SanactionTypeId",
                principalTable: "SanctionTypes",
                principalColumn: "Id");
        }
    }
}
