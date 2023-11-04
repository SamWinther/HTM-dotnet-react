using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTMbackend.Migrations
{
    /// <inheritdoc />
    public partial class introduceEnumRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnumRole",
                table: "userRole",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnumRole",
                table: "userRole");
        }
    }
}
