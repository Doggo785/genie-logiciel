using System;
using System.Collections.Generic;
using System.Threading;

public class Program
{
    public static Philosophe Michel = new Philosophe("Michel");
    public static Philosophe Jean = new Philosophe("Jean");
    public static Philosophe Maude = new Philosophe("Maude");
    public static Philosophe Marie = new Philosophe("Marie");
    public static Philosophe Jacque = new Philosophe("Jacque");
    public static List<Philosophe> philosophes = new List<Philosophe>() { Michel, Jean, Maude, Marie, Jacque };

    public static Baguette[] baguettes = new Baguette[5];

    static void Main(string[] args)
    {
        
        for (int i = 0; i < baguettes.Length; i++)
            baguettes[i] = new Baguette();

        
        Thread[] threads = new Thread[philosophes.Count];
        for (int i = 0; i < philosophes.Count; i++)
        {
            int idx = i; 
            threads[i] = new Thread(() => Dinner(idx));
            threads[i].Start();
        }

        Console.ReadKey();
    }

    static void Dinner(int index)
    {
        var ph = philosophes[index];
        var rnd = new Random();
        int left = index; 
        int right = (index + 1) % baguettes.Length;

        while (true)
        {
            
            ph.Penser();
            Thread.Sleep(rnd.Next(2000));

            
            bool veutManger = rnd.Next(2) == 0;
            if (!veutManger)
                continue;

            Console.WriteLine($"{ph.nom} veut manger.");


            int first = Math.Min(left, right);
            int second = Math.Max(left, right);

            bool aMange = false;

            while (!aMange)
            {
                lock (baguettes[first])
                {
                    lock (baguettes[second])
                    {
                        if (!baguettes[left].estOccupee && !baguettes[right].estOccupee)
                        {
                            baguettes[left].estOccupee = true;
                            baguettes[right].estOccupee = true;
                            Console.WriteLine($"{ph.nom} a pris les baguettes {left} et {right}.");

                            ph.Manger();
                            Thread.Sleep(3000);

                            baguettes[left].estOccupee = false;
                            baguettes[right].estOccupee = false;
                            Console.WriteLine($"{ph.nom} a reposé les baguettes {left} et {right}.");
                            aMange = true;
                        }
                        else
                        {
                            Console.WriteLine($"{ph.nom} attend, baguettes occupées.");
                        }
                    }
                }

                if (!aMange)
                    Thread.Sleep(500);
            }
        }
    }
}

public class Baguette
{
    public bool estOccupee = false;
}
