using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTMbackend.Migrations
{
    /// <inheritdoc />
    public partial class thirdUpdateEnumRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "UserRole_ibfk_3",
                table: "userRole");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                table: "userRole",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "RoleID",
                table: "userRole",
                newName: "IX_userRole_RoleId");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "userRole",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EnumRole",
                table: "userRole",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "EnumRole",
                table: "userRole",
                column: "EnumRole");

            migrationBuilder.AddForeignKey(
                name: "FK_userRole_role_RoleId",
                table: "userRole",
                column: "RoleId",
                principalTable: "role",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRole_role_RoleId",
                table: "userRole");

            migrationBuilder.DropIndex(
                name: "EnumRole",
                table: "userRole");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "userRole",
                newName: "RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_userRole_RoleId",
                table: "userRole",
                newName: "RoleID");

            migrationBuilder.AlterColumn<int>(
                name: "RoleID",
                table: "userRole",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnumRole",
                table: "userRole",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "UserRole_ibfk_3",
                table: "userRole",
                column: "RoleID",
                principalTable: "role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
