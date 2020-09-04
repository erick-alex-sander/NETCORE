using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCORE.Migrations
{
    public partial class addRoleModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_role_tb_m_user_UserId",
                table: "tb_m_role");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_role_UserId",
                table: "tb_m_role");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tb_m_role");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "tb_m_user",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_user_RoleId",
                table: "tb_m_user",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_user_tb_m_role_RoleId",
                table: "tb_m_user",
                column: "RoleId",
                principalTable: "tb_m_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_user_tb_m_role_RoleId",
                table: "tb_m_user");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_user_RoleId",
                table: "tb_m_user");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "tb_m_user");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "tb_m_role",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_role_UserId",
                table: "tb_m_role",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_role_tb_m_user_UserId",
                table: "tb_m_role",
                column: "UserId",
                principalTable: "tb_m_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
