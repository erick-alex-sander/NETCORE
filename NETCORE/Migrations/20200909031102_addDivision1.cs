using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCORE.Migrations
{
    public partial class addDivision1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_m_division_Name",
                table: "tb_m_division");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_m_division",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_m_department",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_division_Name",
                table: "tb_m_division",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_m_division_Name",
                table: "tb_m_division");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_m_division",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_m_department",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_division_Name",
                table: "tb_m_division",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
