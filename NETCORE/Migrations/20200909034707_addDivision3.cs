using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCORE.Migrations
{
    public partial class addDivision3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_m_department",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_m_department",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department",
                column: "Name",
                unique: true);
        }
    }
}
