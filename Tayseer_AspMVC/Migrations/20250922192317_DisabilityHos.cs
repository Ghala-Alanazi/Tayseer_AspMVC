using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tayseer_AspMVC.Migrations
{
    /// <inheritdoc />
    public partial class DisabilityHos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisabilityHospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    DisabilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabilityHospitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisabilityHospitals_Disabilities_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisabilityHospitals_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityHospitals_DisabilityId",
                table: "DisabilityHospitals",
                column: "DisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityHospitals_HospitalId",
                table: "DisabilityHospitals",
                column: "HospitalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisabilityHospitals");
        }
    }
}
