using Microsoft.EntityFrameworkCore.Migrations;

namespace CAD_drug_report.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportDrugIncidents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Agency = table.Column<string>(nullable: true),
                    IncidentTypeID = table.Column<int>(nullable: false),
                    Neighborhood = table.Column<string>(nullable: true),
                    PriorityColor = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDrugIncidents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportDrugIncidents");
        }
    }
}
