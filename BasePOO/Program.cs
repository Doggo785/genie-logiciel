namespace BasePOO;

class Program

{

    static void Main(string[] args)

    {
        bool quitter = false;
        Bibliotheque bibliotheque = new Bibliotheque();
        while (!quitter)
        {
            Console.WriteLine("Bienvenue à la bibliothèque !");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1. Ajouter un livre");
            Console.WriteLine("2. Supprimer un livre");
            Console.WriteLine("3. Rechercher un livre");
            Console.WriteLine("4. Emprunter un livre");
            Console.WriteLine("5. Retourner un livre");
            Console.WriteLine("6. Quitter");
            Console.WriteLine("---------------------------------");
            Console.Write("> ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Entrez le titre du livre:");
                    string titre = Console.ReadLine() ?? "";
                    Console.WriteLine("Entrez l'auteur du livre:");
                    string auteur = Console.ReadLine() ?? "";
                    Livre nouveauLivre = new Livre(titre, auteur);
                    bibliotheque.AjouterLivre(nouveauLivre);
                    Console.WriteLine("Livre ajouté avec succès.");
                    Console.WriteLine(nouveauLivre);
                    break;
                case 2:
                    Console.WriteLine("Entrez le titre du livre à supprimer:");
                    string titreASupprimer = Console.ReadLine() ?? "";
                    bibliotheque.SupprimerLivre(titreASupprimer);
                    Console.WriteLine("Livre supprimé avec succès.");
                    break;
                case 3:
                    Console.WriteLine("Entrez le titre du livre à rechercher:");
                    string titreARechercher = Console.ReadLine() ?? "";
                    Livre livreTrouve = bibliotheque.RechercherLivre(titreARechercher);
                    if (livreTrouve != null)
                    {
                        Console.WriteLine(livreTrouve.ToString());
                    }
                    break;
                case 4:
                    Console.WriteLine("Entrez le titre du livre à emprunter:");
                    string titreAEmprunter = Console.ReadLine() ?? "";
                    Console.WriteLine("Entrez votre nom:");
                    string nomEmprunteur = Console.ReadLine() ?? "";
                    bibliotheque.EmprunterLivre(titreAEmprunter, nomEmprunteur);
                    break;
                case 5:
                    Console.WriteLine("Entrez le titre du livre à retourner:");
                    string titreARetourner = Console.ReadLine() ?? "";
                    bibliotheque.RetournerLivre(titreARetourner);
                    break;
                case 6:
                    quitter = true;
                    Console.WriteLine("Merci d'avoir utilisé la bibliothèque. Au revoir!");
                    break;
            }
            Thread.Sleep(1500);
            Console.Clear();
        }
    }
}