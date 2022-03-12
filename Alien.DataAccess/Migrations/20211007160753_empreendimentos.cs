using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alien.DataAccess.Migrations
{
    public partial class empreendimentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Flg_destaque",
                table: "AlienDB_Imovel",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Regiao",
                table: "AlienDB_Imovel",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Regiao",
                table: "AlienDB_Cadastre_Seu_Imovel",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AlienDB_Empreendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Empresa = table.Column<int>(type: "int", nullable: false),
                    Id_Tipo_Imovel = table.Column<int>(type: "int", nullable: false),
                    NomeLancamento = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cidade = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Construtura = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Regiao = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlienDB_Empreendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlienDB_Empreendimentos_AlienDB_Tipo_Imovel_Id_Tipo_Imovel",
                        column: x => x.Id_Tipo_Imovel,
                        principalTable: "AlienDB_Tipo_Imovel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AlienDB_Empreendimentos_Id_Tipo_Imovel",
                table: "AlienDB_Empreendimentos",
                column: "Id_Tipo_Imovel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlienDB_Empreendimentos");

            migrationBuilder.DropColumn(
                name: "Flg_destaque",
                table: "AlienDB_Imovel");

            migrationBuilder.DropColumn(
                name: "Regiao",
                table: "AlienDB_Imovel");

            migrationBuilder.DropColumn(
                name: "Regiao",
                table: "AlienDB_Cadastre_Seu_Imovel");
        }
    }
}
