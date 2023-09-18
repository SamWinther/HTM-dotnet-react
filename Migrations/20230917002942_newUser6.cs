using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTMbackend.Migrations
{
    /// <inheritdoc />
    public partial class newUser6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "user",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "Username",
                table: "user",
                column: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Username",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "user");
        }
    }
}
