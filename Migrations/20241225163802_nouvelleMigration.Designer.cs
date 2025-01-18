﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace gestioncommande.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241225163802_nouvelleMigration")]
    partial class nouvelleMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("gestioncommande.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createat");

                    b.Property<double>("Solde")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateat");

                    b.Property<string>("addresse")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("usersId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("usersId");

                    b.ToTable("client");
                });

            modelBuilder.Entity("gestioncommande.Models.Commande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CLientId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createat");

                    b.Property<DateTime>("DateCommande")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("PrixTotal")
                        .HasColumnType("double precision");

                    b.Property<bool>("Taken")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateat");

                    b.Property<int>("etatCommande")
                        .HasColumnType("integer");

                    b.Property<bool>("hasPayed")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CLientId");

                    b.ToTable("commande");
                });

            modelBuilder.Entity("gestioncommande.Models.DetailCommande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createat");

                    b.Property<double>("Prix")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateat");

                    b.Property<int>("commandeId")
                        .HasColumnType("integer");

                    b.Property<int>("produitId")
                        .HasColumnType("integer");

                    b.Property<double>("qteCommande")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("commandeId");

                    b.HasIndex("produitId");

                    b.ToTable("detailCommande");
                });

            modelBuilder.Entity("gestioncommande.Models.Livraison", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createat");

                    b.Property<int>("LivreurId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateat");

                    b.Property<string>("addresse")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("commandeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LivreurId");

                    b.HasIndex("commandeId");

                    b.ToTable("livraison");
                });

            modelBuilder.Entity("gestioncommande.Models.Livreur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createat");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateat");

                    b.Property<bool>("estDisponible")
                        .HasColumnType("boolean");

                    b.Property<string>("telephone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("livreur");
                });

            modelBuilder.Entity("gestioncommande.Models.Paiement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createat");

                    b.Property<int>("TypePaiement")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateat");

                    b.Property<int>("commandeId")
                        .HasColumnType("integer");

                    b.Property<bool>("hasReduced")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("commandeId");

                    b.ToTable("paiement");
                });

            modelBuilder.Entity("gestioncommande.Models.Produit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createat");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Prix")
                        .HasColumnType("double precision");

                    b.Property<double>("QteSeuil")
                        .HasColumnType("double precision");

                    b.Property<double>("Quantite")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateat");

                    b.HasKey("Id");

                    b.ToTable("produit");
                });

            modelBuilder.Entity("gestioncommande.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createat");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateat");

                    b.Property<string>("telephone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("gestioncommande.Models.Client", b =>
                {
                    b.HasOne("gestioncommande.Models.User", "users")
                        .WithMany()
                        .HasForeignKey("usersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("users");
                });

            modelBuilder.Entity("gestioncommande.Models.Commande", b =>
                {
                    b.HasOne("gestioncommande.Models.Client", "Client")
                        .WithMany("Commandes")
                        .HasForeignKey("CLientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("gestioncommande.Models.DetailCommande", b =>
                {
                    b.HasOne("gestioncommande.Models.Commande", "commande")
                        .WithMany("detailCommandes")
                        .HasForeignKey("commandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gestioncommande.Models.Produit", "produit")
                        .WithMany("detailCommandes")
                        .HasForeignKey("produitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("commande");

                    b.Navigation("produit");
                });

            modelBuilder.Entity("gestioncommande.Models.Livraison", b =>
                {
                    b.HasOne("gestioncommande.Models.Livreur", "livreur")
                        .WithMany()
                        .HasForeignKey("LivreurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gestioncommande.Models.Commande", "commande")
                        .WithMany()
                        .HasForeignKey("commandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("commande");

                    b.Navigation("livreur");
                });

            modelBuilder.Entity("gestioncommande.Models.Paiement", b =>
                {
                    b.HasOne("gestioncommande.Models.Commande", "commande")
                        .WithMany()
                        .HasForeignKey("commandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("commande");
                });

            modelBuilder.Entity("gestioncommande.Models.Client", b =>
                {
                    b.Navigation("Commandes");
                });

            modelBuilder.Entity("gestioncommande.Models.Commande", b =>
                {
                    b.Navigation("detailCommandes");
                });

            modelBuilder.Entity("gestioncommande.Models.Produit", b =>
                {
                    b.Navigation("detailCommandes");
                });
#pragma warning restore 612, 618
        }
    }
}
