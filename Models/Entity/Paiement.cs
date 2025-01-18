using gestioncommande.Enum;
namespace gestioncommande.Models;
public class Paiement : AbstractEntity
{
    public TypePaiement TypePaiement { get; set; }
    public Commande commande { get; set; }

    public int commandeId { get; set; }

    public string reference { get; set; }

    public bool hasReduced { get; set; } = false;
}
