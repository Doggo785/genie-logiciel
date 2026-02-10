using System.Diagnostics;
using System.Xml.Serialization;

namespace GestionDesProcessus;

class Program
{
    static void ProcessExitHandler(object? sender, EventArgs e)
    {
        Console.WriteLine("Le processus est terminé.");
    }

    static void Main(string[] args)
    {
        LancerProcessus("explorer.exe", @"C:\Windows");
        LancerProcessus("notepad.exe", @"C:\Windows\win.ini");
    }
    static void LancerProcessus(string nomProcessus, string param = "")
    {
        Process processus = new Process();
        processus.EnableRaisingEvents = true;
        processus.Exited += ProcessExitHandler;
        processus.StartInfo.FileName = nomProcessus;
        processus.StartInfo.Arguments = param;

        try
        {
            bool Launched = processus.Start();
            if (Launched)
            {
                Console.WriteLine($"Processus pid : '{processus.Id}', '{nomProcessus}' lancé avec succès.");
                Thread.Sleep(3000);
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