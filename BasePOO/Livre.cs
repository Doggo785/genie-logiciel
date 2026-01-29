namespace BasePOO;
public class Livre
{
    public string titre { get; }
    string auteur;
	public bool estDisponible { get; set; } = true;
	public string NomEmprunteur { get; set; }
	public DateTime DateEmprunt { get; set; } = new DateTime();
    public DateTime DateRetour
    {
        get { return DateEmprunt.AddDays(14); }
    }

    public Livre(string titreLivre, string auteurLivre)
	{
		titre = titreLivre;
		auteur = auteurLivre;
    }

    public override string ToString()
	{
		string informations = "Titre: " + titre + ", Auteur: " + auteur;
		if (!estDisponible)
		{
			informations += ", Emprunté par: " + NomEmprunteur + ", Date d'emprunt: " + DateEmprunt.ToShortDateString() + ", Date de retour prévue: " + DateRetour.ToShortDateString();
        }
		else
		{
			informations += ", Disponible";
        }
        return informations;
    }
}
