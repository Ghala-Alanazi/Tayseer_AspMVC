using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tayseer_AspMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddDisabilityCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisabilityCenters_Centers_CentersId",
                table: "DisabilityCenters");

            migrationBuilder.DropColumn(
                name: "CenterslId",
                table: "DisabilityCenters");

            migrationBuilder.AlterColumn<int>(
                name: "CentersId",
                table: "DisabilityCenters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DisabilityCenters_Centers_CentersId",
                table: "DisabilityCenters",
                column: "CentersId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisabilityCenters_Centers_CentersId",
                table: "DisabilityCenters");

            migrationBuilder.AlterColumn<int>(
                name: "CentersId",
                table: "DisabilityCenters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CenterslId",
                table: "DisabilityCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DisabilityCenters_Centers_CentersId",
                table: "DisabilityCenters",
                column: "CentersId",
                principalTable: "Centers",
                principalColumn: "Id");
        }
    }
}
