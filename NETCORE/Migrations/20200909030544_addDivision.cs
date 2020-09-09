using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCORE.Migrations
{
    public partial class addDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_m_department",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "tb_m_division",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: false),
                    isDelete = table.Column<bool>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_division", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_division_tb_m_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "tb_m_department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_division_DepartmentId",
                table: "tb_m_division",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_division_Name",
                table: "tb_m_division",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_division");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_department_Name",
                table: "tb_m_department");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_m_department",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
