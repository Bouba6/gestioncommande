using System.ComponentModel.DataAnnotations.Schema;
using gestioncommande.Enum;

namespace gestioncommande.Models;
public class Commande : AbstractEntity
{
    public DateTime DateCommande { get; set; }
    public double PrixTotal { get; set; }
    public Client Client { get; set; }
    public int CLientId { get; set; }


    [NotMapped]
    public Livraison? Livraison { get; set; }

    [NotMapped]
    public int LivraisonId { get; set; }


    public List<DetailCommande> detailCommandes { get; set; } = new List<DetailCommande>();

    public bool hasPayed { get; set; } = false;

    public bool Taken { get; set; } = false;

    public EtatCommande etatCommande { get; set; } = EtatCommande.MISENATTENTE;


    [NotMapped]
    public Paiement? Paiement { get; set; }

    [NotMapped]
    public int? PaiementId { get; set; }


}
