using System.ComponentModel.DataAnnotations.Schema;

namespace gestioncommande.Models;

public class Produit : AbstractEntity
{
    public string Libelle { get; set; }
    public double Prix { get; set; }
    public double Quantite { get; set; }

    public string? FileName { get; set; }

    public string? Description { get; set; }

    public double QteSeuil { get; set; }

    [NotMapped]
    public IFormFile? image { get; set; }


    public List<DetailCommande>? detailCommandes { get; set; }

}
