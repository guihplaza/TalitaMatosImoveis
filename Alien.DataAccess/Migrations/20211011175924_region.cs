using Microsoft.EntityFrameworkCore.Migrations;

namespace Alien.DataAccess.Migrations
{
    public partial class region : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlienDB_Cadastre_Seu_Imovel_AlienDAliemDB_RegiaoB_Empreendim~",
                table: "AlienDB_Cadastre_Seu_Imovel");

            migrationBuilder.DropForeignKey(
                name: "FK_AlienDB_Empreendimentos_AlienDAliemDB_RegiaoB_Empreendimento~",
                table: "AlienDB_Empreendimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_AlienDB_Imovel_AlienDAliemDB_RegiaoB_Empreendimentos_Id_Regi~",
                table: "AlienDB_Imovel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlienDAliemDB_RegiaoB_Empreendimentos",
                table: "AlienDAliemDB_RegiaoB_Empreendimentos");

            migrationBuilder.RenameTable(
                name: "AlienDAliemDB_RegiaoB_Empreendimentos",
                newName: "AlienDB_Regiao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlienDB_Regiao",
                table: "AlienDB_Regiao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlienDB_Cadastre_Seu_Imovel_AlienDB_Regiao_Id_Regiao",
                table: "AlienDB_Cadastre_Seu_Imovel",
                column: "Id_Regiao",
                principalTable: "AlienDB_Regiao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlienDB_Empreendimentos_AlienDB_Regiao_Id_Regiao",
                table: "AlienDB_Empreendimentos",
                column: "Id_Regiao",
                principalTable: "AlienDB_Regiao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlienDB_Imovel_AlienDB_Regiao_Id_Regiao",
                table: "AlienDB_Imovel",
                column: "Id_Regiao",
                principalTable: "AlienDB_Regiao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlienDB_Cadastre_Seu_Imovel_AlienDB_Regiao_Id_Regiao",
                table: "AlienDB_Cadastre_Seu_Imovel");

            migrationBuilder.DropForeignKey(
                name: "FK_AlienDB_Empreendimentos_AlienDB_Regiao_Id_Regiao",
                table: "AlienDB_Empreendimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_AlienDB_Imovel_AlienDB_Regiao_Id_Regiao",
                table: "AlienDB_Imovel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlienDB_Regiao",
                table: "AlienDB_Regiao");

            migrationBuilder.RenameTable(
                name: "AlienDB_Regiao",
                newName: "AlienDAliemDB_RegiaoB_Empreendimentos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlienDAliemDB_RegiaoB_Empreendimentos",
                table: "AlienDAliemDB_RegiaoB_Empreendimentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlienDB_Cadastre_Seu_Imovel_AlienDAliemDB_RegiaoB_Empreendim~",
                table: "AlienDB_Cadastre_Seu_Imovel",
                column: "Id_Regiao",
                principalTable: "AlienDAliemDB_RegiaoB_Empreendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlienDB_Empreendimentos_AlienDAliemDB_RegiaoB_Empreendimento~",
                table: "AlienDB_Empreendimentos",
                column: "Id_Regiao",
                principalTable: "AlienDAliemDB_RegiaoB_Empreendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlienDB_Imovel_AlienDAliemDB_RegiaoB_Empreendimentos_Id_Regi~",
                table: "AlienDB_Imovel",
                column: "Id_Regiao",
                principalTable: "AlienDAliemDB_RegiaoB_Empreendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
