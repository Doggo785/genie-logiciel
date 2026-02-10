using System.Diagnostics;
using System.Xml.Serialization;

namespace GestionDesProcessus;

class Program
{
    static void Main(string[] args)
    {
        //LancerProcessus("notepad.exe");
        //LancerProcessus("explorer.exe");
        Process processus = new Process();
        processus.StartInfo.FileName = "explorer.exe";
        processus.StartInfo.Arguments = @"C:\Windows";
        processus.Start();
        Thread.Sleep(2000);
        processus.WaitForExit();

        //Console.ReadKey();
    }
    static void LancerProcessus(string nomProcessus)
    {
        Process processus = new Process();
        processus.StartInfo.FileName = nomProcessus;


        try
        {
            bool Launched = processus.Start();
            if (Launched)
            {
                Console.WriteLine($"Processus '{nomProcessus}' lancé avec succès.");
                processus.WaitForExit();
            }
            else
            {
                Console.WriteLine($"Échec du lancement du processus '{nomProcessus}'.");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du lancement du processus: {ex.Message}");
        }
    }
}