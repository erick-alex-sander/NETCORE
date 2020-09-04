using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCORE.Migrations
{
    public partial class addRoleModel4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "tb_m_userrole",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_userrole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tb_m_userrole_tb_m_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_userrole_tb_m_user_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_m_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_userrole_RoleId",
                table: "tb_m_userrole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_userrole");

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
    }
}
