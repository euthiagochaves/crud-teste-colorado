using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crude_teste.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CodigoCliente = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RazaoSocial = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NomeFantasia = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TipoPessoa = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    Documento = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Complemento = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CEP = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    UF = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioInsercao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.CodigoCliente);
                });

            migrationBuilder.CreateTable(
                name: "TiposTelefone",
                columns: table => new
                {
                    CodigoTipoTelefone = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescricaoTipoTelefone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioInsercao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposTelefone", x => x.CodigoTipoTelefone);
                });

            migrationBuilder.CreateTable(
                name: "Telefones",
                columns: table => new
                {
                    CodigoCliente = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroTelefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CodigoTipoTelefone = table.Column<int>(type: "INTEGER", nullable: false),
                    Operadora = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioInsercao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefones", x => new { x.CodigoCliente, x.NumeroTelefone });
                    table.ForeignKey(
                        name: "FK_Telefones_Clientes_CodigoCliente",
                        column: x => x.CodigoCliente,
                        principalTable: "Clientes",
                        principalColumn: "CodigoCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Telefones_TiposTelefone_CodigoTipoTelefone",
                        column: x => x.CodigoTipoTelefone,
                        principalTable: "TiposTelefone",
                        principalColumn: "CodigoTipoTelefone",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TiposTelefone",
                columns: new[] { "CodigoTipoTelefone", "DataInsercao", "DescricaoTipoTelefone", "UsuarioInsercao" },
                values: new object[] { 1, new DateTime(2025, 5, 16, 12, 47, 47, 345, DateTimeKind.Local).AddTicks(7907), "RESIDENCIAL", "SYSTEM" });

            migrationBuilder.InsertData(
                table: "TiposTelefone",
                columns: new[] { "CodigoTipoTelefone", "DataInsercao", "DescricaoTipoTelefone", "UsuarioInsercao" },
                values: new object[] { 2, new DateTime(2025, 5, 16, 12, 47, 47, 345, DateTimeKind.Local).AddTicks(7917), "COMERCIAL", "SYSTEM" });

            migrationBuilder.InsertData(
                table: "TiposTelefone",
                columns: new[] { "CodigoTipoTelefone", "DataInsercao", "DescricaoTipoTelefone", "UsuarioInsercao" },
                values: new object[] { 3, new DateTime(2025, 5, 16, 12, 47, 47, 345, DateTimeKind.Local).AddTicks(7918), "WHATSAPP", "SYSTEM" });

            migrationBuilder.InsertData(
                table: "TiposTelefone",
                columns: new[] { "CodigoTipoTelefone", "DataInsercao", "DescricaoTipoTelefone", "UsuarioInsercao" },
                values: new object[] { 4, new DateTime(2025, 5, 16, 12, 47, 47, 345, DateTimeKind.Local).AddTicks(7919), "CELULAR", "SYSTEM" });

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_CodigoTipoTelefone",
                table: "Telefones",
                column: "CodigoTipoTelefone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telefones");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "TiposTelefone");
        }
    }
}
