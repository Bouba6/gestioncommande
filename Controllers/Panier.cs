using gestioncommande.Models;
using gestioncommande.Models;
using Microsoft.IdentityModel.Tokens;
namespace gestioncommande.Controllers;

public class Panier
{
    // private readonly ApplicationDbContext _context;

    // public Panier(ApplicationDbContext context)
    // {
    //     _context = context ?? throw new ArgumentNullException(nameof(context));
    // }
    public List<DetailCommande> detailCommande { get; set; } = new List<DetailCommande>();
    public double PrixTotal;
    public Client client;
    public int nbreProduits;
    public double qte = 0.0;
    public int PrixTotalAvecreduction;
    public int _idTemporaireCounter = 0;

    public void AjouterProduits(Produit produit1, double qte)
    {

        Console.WriteLine(PrixTotal);
        // Vérifier si l'article existe déjà dans le panier
        DetailCommande existingDetail = detailCommande.FirstOrDefault(d => d.produit.Id == produit1.Id);

        if (existingDetail != null)
        {
            existingDetail.qteCommande = qte;
            existingDetail.Prix = produit1.Prix * existingDetail.qteCommande;
        }
        else
        {
            _idTemporaireCounter++;
            double prix = produit1.Prix * qte;
            detailCommande.Add(new DetailCommande { IdTemporaire = _idTemporaireCounter, produit = produit1, qteCommande = qte, Prix = prix });
        }

    }

    public void PrixTotalCommande()
    {
        PrixTotal = 0;
        nbreProduits = 0;
        foreach (DetailCommande detail in detailCommande)
        {
            nbreProduits += 1;
            PrixTotal = PrixTotal + detail.Prix;
        }


    }


    public void deleteDetail(int id)
    {
        var detail = detailCommande.FirstOrDefault(d => d.IdTemporaire == id);
        if (detail != null)
        {
            detailCommande.Remove(detail);
            Console.WriteLine($"Détail supprimé : {detail}");
        }
        else
        {
            Console.WriteLine($"Aucun détail trouvé pour l'ID : {id}");
        }

    }


}