using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace gestioncommande.Migrations
{
    /// <inheritdoc />
    public partial class nouvelleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "livreur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estDisponible = table.Column<bool>(type: "boolean", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    telephone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livreur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "produit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Libelle = table.Column<string>(type: "text", nullable: false),
                    Prix = table.Column<double>(type: "double precision", nullable: false),
                    Quantite = table.Column<double>(type: "double precision", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    QteSeuil = table.Column<double>(type: "double precision", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    telephone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Solde = table.Column<double>(type: "double precision", nullable: false),
                    addresse = table.Column<string>(type: "text", nullable: false),
                    usersId = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_users_usersId",
                        column: x => x.usersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "commande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCommande = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PrixTotal = table.Column<double>(type: "double precision", nullable: false),
                    CLientId = table.Column<int>(type: "integer", nullable: false),
                    hasPayed = table.Column<bool>(type: "boolean", nullable: false),
                    Taken = table.Column<bool>(type: "boolean", nullable: false),
                    etatCommande = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_commande_client_CLientId",
                        column: x => x.CLientId,
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detailCommande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    qteCommande = table.Column<double>(type: "double precision", nullable: false),
                    commandeId = table.Column<int>(type: "integer", nullable: false),
                    Prix = table.Column<double>(type: "double precision", nullable: false),
                    produitId = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detailCommande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detailCommande_commande_commandeId",
                        column: x => x.commandeId,
                        principalTable: "commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detailCommande_produit_produitId",
                        column: x => x.produitId,
                        principalTable: "produit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "livraison",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    commandeId = table.Column<int>(type: "integer", nullable: false),
                    addresse = table.Column<string>(type: "text", nullable: false),
                    LivreurId = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livraison", x => x.Id);
                    table.ForeignKey(
                        name: "FK_livraison_commande_commandeId",
                        column: x => x.commandeId,
                        principalTable: "commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_livraison_livreur_LivreurId",
                        column: x => x.LivreurId,
                        principalTable: "livreur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "paiement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypePaiement = table.Column<int>(type: "integer", nullable: false),
                    commandeId = table.Column<int>(type: "integer", nullable: false),
                    hasReduced = table.Column<bool>(type: "boolean", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paiement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paiement_commande_commandeId",
                        column: x => x.commandeId,
                        principalTable: "commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_client_usersId",
                table: "client",
                column: "usersId");

            migrationBuilder.CreateIndex(
                name: "IX_commande_CLientId",
                table: "commande",
                column: "CLientId");

            migrationBuilder.CreateIndex(
                name: "IX_detailCommande_commandeId",
                table: "detailCommande",
                column: "commandeId");

            migrationBuilder.CreateIndex(
                name: "IX_detailCommande_produitId",
                table: "detailCommande",
                column: "produitId");

            migrationBuilder.CreateIndex(
                name: "IX_livraison_commandeId",
                table: "livraison",
                column: "commandeId");

            migrationBuilder.CreateIndex(
                name: "IX_livraison_LivreurId",
                table: "livraison",
                column: "LivreurId");

            migrationBuilder.CreateIndex(
                name: "IX_paiement_commandeId",
                table: "paiement",
                column: "commandeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detailCommande");

            migrationBuilder.DropTable(
                name: "livraison");

            migrationBuilder.DropTable(
                name: "paiement");

            migrationBuilder.DropTable(
                name: "produit");

            migrationBuilder.DropTable(
                name: "livreur");

            migrationBuilder.DropTable(
                name: "commande");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
