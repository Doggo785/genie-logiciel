using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace GestionDesProcessus;

public class ProcessViewItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Priority { get; set; } = "N/A"; 
    public long VirtualMemory { get; set; }
    public Process SourceProcess { get; set; } 
}

public partial class MainWindow : Window
{
    private readonly string[] _criticalSystems = {
        "svchost", "csrss", "wininit", "smss", "services", "lsass", "winlogon", "System", "Idle"
    };

    public MainWindow()
    {
        InitializeComponent();
        ChargerProcessus();
    }

    private void ChargerProcessus()
    {
        var listeAffichage = new List<ProcessViewItem>();
        Process[] processes = Process.GetProcesses();

        foreach (var p in processes)
        {
            var item = new ProcessViewItem
            {
                Id = p.Id,
                Name = p.ProcessName,
                SourceProcess = p
            };


            try
            {
                item.VirtualMemory = p.VirtualMemorySize64;
                item.Priority = p.BasePriority.ToString();
            }
            catch (Win32Exception)
            {
                item.Priority = "Accès Refusé";
            }
            catch (InvalidOperationException)
            {
                item.Priority = "Terminé";
            }

            listeAffichage.Add(item);
        }

        dgProcessus.ItemsSource = listeAffichage.OrderBy(x => x.Name).ToList();
    }

    private void BtnLancer_Click(object sender, RoutedEventArgs e)
    {
        string nomExe = txtProcessName.Text.Trim();
        if (string.IsNullOrWhiteSpace(nomExe)) return;

        try
        {
            Process.Start(nomExe);
            System.Threading.Tasks.Task.Delay(500).ContinueWith(_ => Dispatcher.Invoke(ChargerProcessus));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Impossible de lancer '{nomExe}' : \n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnArreter_Click(object sender, RoutedEventArgs e)
    {

        if (dgProcessus.SelectedItem is not ProcessViewItem selectedItem)
        {
            MessageBox.Show("Veuillez sélectionner un processus.", "Info");
            return;
        }

        if (_criticalSystems.Contains(selectedItem.Name, StringComparer.OrdinalIgnoreCase))
        {
            MessageBox.Show($"INTERDIT : '{selectedItem.Name}' est un processus système critique.\nL'arrêter ferait planter Windows.",
                            "Sécurité", MessageBoxButton.OK, MessageBoxImage.Stop);
            return;
        }


        var result = MessageBox.Show($"Voulez-vous vraiment tuer le processus '{selectedItem.Name}' (PID: {selectedItem.Id}) ?",
                                     "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                selectedItem.SourceProcess.Kill();
                MessageBox.Show("Processus arrêté.");
                ChargerProcessus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'arrêt : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BtnQuitter_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        ChargerProcessus();
    }
}