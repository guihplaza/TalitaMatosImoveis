using Microsoft.EntityFrameworkCore.Migrations;

namespace Alien.DataAccess.Migrations
{
    public partial class flgdestaque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Flg_destaque",
                table: "AlienDB_Imovel",
                type: "varchar(3)",
                maxLength: 3,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flg_destaque",
                table: "AlienDB_Imovel");
        }
    }
}
