using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tayseer_AspMVC.Migrations
{
    /// <inheritdoc />
    public partial class SchoolDisability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Staages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolDisabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    DisabilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolDisabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolDisabilities_Disabilities_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolDisabilities_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolDisabilities_DisabilityId",
                table: "SchoolDisabilities",
                column: "DisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolDisabilities_SchoolId",
                table: "SchoolDisabilities",
                column: "SchoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolDisabilities");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
