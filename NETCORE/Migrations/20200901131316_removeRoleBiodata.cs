using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCORE.Migrations
{
    public partial class removeRoleBiodata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_role_tb_m_biodata_BiodataId",
                table: "tb_m_role");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_role_BiodataId",
                table: "tb_m_role");

            migrationBuilder.DropColumn(
                name: "BiodataId",
                table: "tb_m_role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BiodataId",
                table: "tb_m_role",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_role_BiodataId",
                table: "tb_m_role",
                column: "BiodataId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_role_tb_m_biodata_BiodataId",
                table: "tb_m_role",
                column: "BiodataId",
                principalTable: "tb_m_biodata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
