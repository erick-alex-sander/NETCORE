using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCORE.Migrations
{
    public partial class addInitDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biodatas_tb_m_user_Id",
                table: "Biodatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Biodatas",
                table: "Biodatas");

            migrationBuilder.RenameTable(
                name: "Biodatas",
                newName: "tb_m_biodata");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_biodata",
                table: "tb_m_biodata",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_biodata_tb_m_user_Id",
                table: "tb_m_biodata",
                column: "Id",
                principalTable: "tb_m_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_biodata_tb_m_user_Id",
                table: "tb_m_biodata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_biodata",
                table: "tb_m_biodata");

            migrationBuilder.RenameTable(
                name: "tb_m_biodata",
                newName: "Biodatas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Biodatas",
                table: "Biodatas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Biodatas_tb_m_user_Id",
                table: "Biodatas",
                column: "Id",
                principalTable: "tb_m_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
