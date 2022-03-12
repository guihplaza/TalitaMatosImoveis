using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alien.DataAccess.Migrations
{
    public partial class migracaoinicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AlienDB_Empresa",
                columns: table => new
                {
                    Id_Empresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Des_Empresa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Flg_Ativo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Des_Contato = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlienDB_Empresa", x => x.Id_Empresa);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AlienDB_Tipo_Imovel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Empresa = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlgTipoImovel = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlienDB_Tipo_Imovel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AlienDB_Usuario_Sistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom_Login = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nom_Completo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Empresa = table.Column<int>(type: "int", nullable: false),
                    Des_Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Des_Senha = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dat_Hora_Fim = table.Column<DateTime>(type: "datetime", nullable: true),
                    Des_Tag = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nro_CPF = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nro_RG = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Des_Endereco = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Des_Telefone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Des_Cidade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Des_UF = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlienDB_Usuario_Sistema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlienDB_Usuario_Sistema_AlienDB_Empresa_Id_Empresa",
                        column: x => x.Id_Empresa,
                        principalTable: "AlienDB_Empresa",
                        principalColumn: "Id_Empresa",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AlienDB_Cadastre_Seu_Imovel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Empresa = table.Column<int>(type: "int", nullable: false),
                    Id_Tipo_Imovel = table.Column<int>(type: "int", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bairro = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cidade = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor_aluguel = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Tel_contato = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome_proprietario = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlienDB_Cadastre_Seu_Imovel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlienDB_Cadastre_Seu_Imovel_AlienDB_Tipo_Imovel_Id_Tipo_Imov~",
                        column: x => x.Id_Tipo_Imovel,
                        principalTable: "AlienDB_Tipo_Imovel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AlienDB_Imovel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Empresa = table.Column<int>(type: "int", nullable: false),
                    Id_Tipo_Imovel = table.Column<int>(type: "int", nullable: false),
                    Codigo_chave = table.Column<int>(type: "int", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bairro = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cidade = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Qtd_Dormitorios = table.Column<int>(type: "int", nullable: true),
                    Qtd_Sala_estar = table.Column<int>(type: "int", nullable: true),
                    Qtd_Sala_jantar = table.Column<int>(type: "int", nullable: true),
                    Qtd_Banheiro = table.Column<int>(type: "int", nullable: true),
                    Qtd_Lavanderia = table.Column<int>(type: "int", nullable: true),
                    Qtd_Edicula = table.Column<int>(type: "int", nullable: true),
                    Qtd_Suite = table.Column<int>(type: "int", nullable: true),
                    Qtd_Copa = table.Column<int>(type: "int", nullable: true),
                    Qtd_Carro_Garagem = table.Column<int>(type: "int", nullable: true),
                    Qtd_Dispensa = table.Column<int>(type: "int", nullable: true),
                    Qtd_Quintal = table.Column<int>(type: "int", nullable: true),
                    Qtd_Aluguel_Advogado = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Qtd_Aluguel_Pintura = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Nro_Parcela_Inicio_Aluguel_Imobiliaria = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Observacoes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor_IPTU = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor_condominio = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor_Taxa_Administrativa = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor_aluguel = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Condicoes_Imovel = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dat_saida_locacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    Dat_Inclui = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlienDB_Imovel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlienDB_Imovel_AlienDB_Tipo_Imovel_Id_Tipo_Imovel",
                        column: x => x.Id_Tipo_Imovel,
                        principalTable: "AlienDB_Tipo_Imovel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AlienDB_Midia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Empresa = table.Column<int>(type: "int", nullable: false),
                    Id_Imovel = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Caminho = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemByte = table.Column<byte[]>(type: "Binary", maxLength: 16, nullable: true),
                    ContentType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlienDB_Midia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlienDB_Midia_AlienDB_Imovel_Id_Imovel",
                        column: x => x.Id_Imovel,
                        principalTable: "AlienDB_Imovel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AlienDB_Cadastre_Seu_Imovel_Id_Tipo_Imovel",
                table: "AlienDB_Cadastre_Seu_Imovel",
                column: "Id_Tipo_Imovel");

            migrationBuilder.CreateIndex(
                name: "IX_AlienDB_Imovel_Id_Tipo_Imovel",
                table: "AlienDB_Imovel",
                column: "Id_Tipo_Imovel");

            migrationBuilder.CreateIndex(
                name: "IX_AlienDB_Midia_Id_Imovel",
                table: "AlienDB_Midia",
                column: "Id_Imovel");

            migrationBuilder.CreateIndex(
                name: "IX_AlienDB_Usuario_Sistema_Id_Empresa",
                table: "AlienDB_Usuario_Sistema",
                column: "Id_Empresa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlienDB_Cadastre_Seu_Imovel");

            migrationBuilder.DropTable(
                name: "AlienDB_Midia");

            migrationBuilder.DropTable(
                name: "AlienDB_Usuario_Sistema");

            migrationBuilder.DropTable(
                name: "AlienDB_Imovel");

            migrationBuilder.DropTable(
                name: "AlienDB_Empresa");

            migrationBuilder.DropTable(
                name: "AlienDB_Tipo_Imovel");
        }
    }
}
