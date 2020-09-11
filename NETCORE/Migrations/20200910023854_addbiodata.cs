using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCORE.Migrations
{
    public partial class addbiodata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "tb_m_biodata",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "tb_m_biodata",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "tb_m_biodata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "tb_m_biodata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "tb_m_biodata",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "tb_m_biodata");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "tb_m_biodata");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "tb_m_biodata");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "tb_m_biodata");

            migrationBuilder.DropColumn(
                name: "University",
                table: "tb_m_biodata");
        }
    }
}
