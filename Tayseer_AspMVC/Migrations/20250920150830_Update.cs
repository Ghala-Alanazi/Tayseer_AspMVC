using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tayseer_AspMVC.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisabilityHospitals");

            migrationBuilder.DropTable(
                name: "SchoolDisabilitys");

            migrationBuilder.RenameColumn(
                name: "Staages",
                table: "Schools",
                newName: "Stages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stages",
                table: "Schools",
                newName: "Staages");

            migrationBuilder.CreateTable(
                name: "DisabilityHospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisabilityId = table.Column<int>(type: "int", nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "SchoolDisabilitys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisabilityId = table.Column<int>(type: "int", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolDisabilitys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolDisabilitys_Disabilities_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolDisabilitys_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
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

            migrationBuilder.CreateIndex(
                name: "IX_SchoolDisabilitys_DisabilityId",
                table: "SchoolDisabilitys",
                column: "DisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolDisabilitys_SchoolId",
                table: "SchoolDisabilitys",
                column: "SchoolId");
        }
    }
}
