using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tayseer_AspMVC.Migrations
{
    /// <inheritdoc />
    public partial class SchoolDisabilitys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolDisabilities_Disabilities_DisabilityId",
                table: "SchoolDisabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolDisabilities_Schools_SchoolId",
                table: "SchoolDisabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolDisabilities",
                table: "SchoolDisabilities");

            migrationBuilder.RenameTable(
                name: "SchoolDisabilities",
                newName: "SchoolDisabilitys");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolDisabilities_SchoolId",
                table: "SchoolDisabilitys",
                newName: "IX_SchoolDisabilitys_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolDisabilities_DisabilityId",
                table: "SchoolDisabilitys",
                newName: "IX_SchoolDisabilitys_DisabilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolDisabilitys",
                table: "SchoolDisabilitys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolDisabilitys_Disabilities_DisabilityId",
                table: "SchoolDisabilitys",
                column: "DisabilityId",
                principalTable: "Disabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolDisabilitys_Schools_SchoolId",
                table: "SchoolDisabilitys",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolDisabilitys_Disabilities_DisabilityId",
                table: "SchoolDisabilitys");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolDisabilitys_Schools_SchoolId",
                table: "SchoolDisabilitys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolDisabilitys",
                table: "SchoolDisabilitys");

            migrationBuilder.RenameTable(
                name: "SchoolDisabilitys",
                newName: "SchoolDisabilities");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolDisabilitys_SchoolId",
                table: "SchoolDisabilities",
                newName: "IX_SchoolDisabilities_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolDisabilitys_DisabilityId",
                table: "SchoolDisabilities",
                newName: "IX_SchoolDisabilities_DisabilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolDisabilities",
                table: "SchoolDisabilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolDisabilities_Disabilities_DisabilityId",
                table: "SchoolDisabilities",
                column: "DisabilityId",
                principalTable: "Disabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolDisabilities_Schools_SchoolId",
                table: "SchoolDisabilities",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
