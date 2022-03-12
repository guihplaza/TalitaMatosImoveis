using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alien.DataAccess.Migrations
{
    public partial class ImagemPrincipal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Dat_Hora_Fim",
                table: "AlienDB_Usuario_Sistema",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ImagemPrincipal",
                table: "AlienDB_Midia",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dat_saida_locacao",
                table: "AlienDB_Imovel",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dat_Inclui",
                table: "AlienDB_Imovel",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemPrincipal",
                table: "AlienDB_Midia");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dat_Hora_Fim",
                table: "AlienDB_Usuario_Sistema",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dat_saida_locacao",
                table: "AlienDB_Imovel",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dat_Inclui",
                table: "AlienDB_Imovel",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}
