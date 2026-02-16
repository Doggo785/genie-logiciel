using System;

public class Philosophe
{
    public string nom;
    public int numero;

    public Philosophe(string name)
    {
        nom = name;
    }

    public void Manger()
    {
        Console.WriteLine($"{nom} : Je mange.");
    }

    public void Penser()
    {
        Console.WriteLine($"{nom} : Je pense.");
    }
}
