using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTMbackend.Migrations
{
    /// <inheritdoc />
    public partial class ConnectRisk2Project : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "risk",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ProjectId1",
                table: "risk",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "risk_ibfk_1",
                table: "risk",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "risk_ibfk_1",
                table: "risk");

            migrationBuilder.DropIndex(
                name: "ProjectId1",
                table: "risk");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "risk");
        }
    }
}
