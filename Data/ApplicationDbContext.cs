using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using gestioncommande.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }



    public DbSet<gestioncommande.Models.User> users { get; set; } = default!;
    public DbSet<gestioncommande.Models.Client> client { get; set; } = default!;

    public DbSet<gestioncommande.Models.Commande> commande { get; set; } = default!;


    public DbSet<gestioncommande.Models.Produit> produit { get; set; } = default!;

    public DbSet<gestioncommande.Models.DetailCommande> detailCommande { get; set; } = default!;


    public DbSet<gestioncommande.Models.Paiement> paiement { get; set; } = default!;


    public DbSet<gestioncommande.Models.Livreur> livreur { get; set; } = default!;

    public DbSet<gestioncommande.Models.Livraison> livraison { get; set; } = default!;



}
