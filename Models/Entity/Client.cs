using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace gestioncommande.Models;
public class Client : AbstractEntity
{
    public double Solde { get; set; }
    public string addresse { get; set; }

    [JsonIgnore]
    public List<Commande> Commandes { get; set; }

    public User users { get; set; }

}
