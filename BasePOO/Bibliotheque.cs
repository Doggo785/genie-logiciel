namespace BasePOO;
public class Bibliotheque
{
	List<Livre> _catalogue = new List<Livre>();
    public Bibliotheque()
	{
	}
	public void AjouterLivre(Livre nouveauLivre)
	{
		_catalogue.Add(nouveauLivre);
    }

	public void SupprimerLivre(string titre)
	{
		_catalogue.RemoveAll(livre => livre.titre == titre);
    }
	public Livre RechercherLivre(string titre)
	{
		foreach (Livre livre in _catalogue)
		{
			if (livre.titre == titre)
			{
                return livre;
			}
		}
		Console.WriteLine("Livre non trouvé.");
        return null;

    }
    public void EmprunterLivre(string titre, string nomEmprunteur)
	{
		foreach (Livre livre in _catalogue)
		{
			if (livre.titre == titre)
			{
				if (livre.estDisponible)
				{
					livre.estDisponible = false;
					livre.NomEmprunteur = nomEmprunteur;
					livre.DateEmprunt = DateTime.Now;
                    Console.WriteLine("Livre emprunté avec succès.");
				}
				else
				{
					Console.WriteLine("Le livre n'est pas disponible.");
				}
				return;
			}
		}
		Console.WriteLine("Livre non trouvé.");
    }
	public void RetournerLivre(string titre)
	{
		foreach (Livre livre in _catalogue)
		{
			if (livre.titre == titre)
			{
				if (!livre.estDisponible)
				{
					livre.estDisponible = true;
					livre.NomEmprunteur = null;
					livre.DateEmprunt = new DateTime();
					Console.WriteLine("Livre retourné avec succès.");
				}
				else
				{
					Console.WriteLine("Le livre n'était pas emprunté.");
				}
				return;
			}
        }
    }
	public void AfficherLivres()
	{
		foreach (Livre livre in _catalogue)
		{
			Console.WriteLine(livre.ToString());
		}
    }
}
