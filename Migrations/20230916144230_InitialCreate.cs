using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace HTMbackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rcmtype",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "risk",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    hazard = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    designChar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    lifeCycle = table.Column<int>(type: "int", nullable: false),
                    scenario = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    hazardSit = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    harm = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Prob_pre = table.Column<int>(type: "int", nullable: false),
                    severity_pre = table.Column<int>(type: "int", nullable: false),
                    risk_pre = table.Column<int>(type: "int", nullable: false),
                    rcmRational = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Prob_past = table.Column<int>(type: "int", nullable: false),
                    severity_past = table.Column<int>(type: "int", nullable: false),
                    risk_past = table.Column<int>(type: "int", nullable: false),
                    complete = table.Column<byte[]>(type: "binary(1)", fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rcm",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RCMtype = table.Column<int>(type: "int", nullable: false),
                    RCMtext = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    NewRiskFromRCM = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    implement = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    VerOfEff = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "rcm_ibfk_1",
                        column: x => x.RCMtype,
                        principalTable: "rcmtype",
                        principalColumn: "ID");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rcm2risk",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    riskID = table.Column<int>(type: "int", nullable: true),
                    rcmID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "rcm2risk_ibfk_1",
                        column: x => x.rcmID,
                        principalTable: "rcm",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "rcm2risk_ibfk_2",
                        column: x => x.riskID,
                        principalTable: "risk",
                        principalColumn: "ID");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "RCMtype",
                table: "rcm",
                column: "RCMtype");

            migrationBuilder.CreateIndex(
                name: "rcmID",
                table: "rcm2risk",
                column: "rcmID");

            migrationBuilder.CreateIndex(
                name: "riskID",
                table: "rcm2risk",
                column: "riskID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rcm2risk");

            migrationBuilder.DropTable(
                name: "rcm");

            migrationBuilder.DropTable(
                name: "risk");

            migrationBuilder.DropTable(
                name: "rcmtype");
        }
    }
}
