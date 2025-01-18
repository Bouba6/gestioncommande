using gestioncommande.Models;
using gestioncommande.Enum;

namespace gestioncommande.Data.Fixtures
{
    public static class Fixtures
    {
        public static void Initialize(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            // Vérifier si des données existent déjà dans la table Clients
            if (context.users.Any())
            {
                Console.WriteLine("Les données existent déjà.");
                return;
            }

            // Ajouter des clients par défaut
            var Users = new List<User>();
            var clients = new List<Client>();
            var commandes = new List<Commande>();
            var Produits = new List<Produit>();
            var detailCommandes = new List<DetailCommande>();

            // Création des utilisateurs
            for (int i = 0; i < 3; i++)
            {
                var roles = new Role[] { Role.CLIENT, Role.COMPTABLE, Role.RS };
                Users.Add(new User
                {
                    Nom = $"Nom{i + 1}",
                    Prenom = $"Prenom{i + 1}",
                    Login = $"Login{i + 1}",
                    Password = $"Password{i + 1}",
                    telephone = $"telephone{i + 1}",
                    Role = roles[i % roles.Length],
                });
            }

            // Création des clients
            for (int i = 0; i < 3; i++)
            {
                var user = new User
                {
                    telephone = $"telephone{i + 1}",
                    Nom = $"Nom{i + 1}",
                    Prenom = $"Prenom{i + 1}",
                    Login = $"ClientLogin{i + 1}",
                    Password = $"ClientPassword{i + 1}",
                };
                Users.Add(user);

                clients.Add(new Client
                {
                    addresse = $"adresse{i + 1}",
                    users = user,
                });
            }

            // Création des produits
            for (int i = 0; i < 3; i++)
            {
                Produits.Add(new Produit
                {
                    Libelle = $"Produit {i + 1}",
                    Prix = 50.0 * (i + 1),
                    Quantite = 100 * (i + 1),
                    QteSeuil = 100 * (i + 1),

                });
            }

            // Ajout des utilisateurs, clients et produits à la base de données
            context.users.AddRange(Users);
            context.client.AddRange(clients);
            context.produit.AddRange(Produits);
            // context.SaveChanges();

            // Récupérer des clients et des produits existants
            var existingClients = context.client.Take(3).ToList();
            var existingProduits = context.produit.Take(3).ToList(); // Assurer que nous avons des produits

            // Créer des commandes et des détails de commandes
            for (int i = 0; i < 3; i++)
            {
                var commande = new Commande
                {
                    DateCommande = DateTime.UtcNow,
                    PrixTotal = 100 * (i + 1),
                    Client = existingClients[i],
                    CLientId = existingClients[i].Id,
                };

                // Ajouter des produits à la commande sous forme de détails
                for (int j = 0; j < 2; j++) // Ajouter 2 produits par commande
                {
                    var produit = existingProduits[j % existingProduits.Count]; // Sélectionner un produit existant
                    detailCommandes.Add(new DetailCommande
                    {
                        commande = commande,
                        commandeId = commande.Id,
                        produit = produit,
                        produitId = produit.Id,
                        qteCommande = 2,
                        Prix = produit.Prix
                    });
                }

                // Ajouter la commande à la liste
                commandes.Add(commande);
            }

            // Ajouter les commandes et détails de commandes à la base de données
            context.commande.AddRange(commandes);
            context.detailCommande.AddRange(detailCommandes);
            // context.SaveChanges();

            Console.WriteLine("Les commandes et détails de commandes ont été initialisés avec succès.");
        }
    }
}
